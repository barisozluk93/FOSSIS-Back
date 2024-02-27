using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserManagement.DbContexts;
using UserManagement.Entity;
using UserManagement.Interfaces;
using UserManagement.Model;

namespace UserManagement.Services
{
    public class UserService : IUserService
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        private readonly UserManagementContext _dbContext;

        private readonly IConfiguration configuration;


        public UserService(UserManagementContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            this.configuration = configuration;
        }

        public async Task<Result<PagingResult<PagedList<User>>>> Paginate(PagingParameter pagingParameter)
        {
            var result = new Result<PagingResult<PagedList<User>>>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var queryable = _dbContext.Users.Select(s => new User()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Surname = s.Surname,
                        Username = s.Username,
                        IsDeleted = s.IsDeleted,
                        Email = s.Email,
                        Phone = s.Phone,
                        IsSystemData = s.IsSystemData,
                        Permissions = _dbContext.UserPermissions.Include(p => p.Permission).Where(x => !x.IsDeleted && x.UserId == s.Id).Select(p => p.Permission).ToList(),
                        Roles = _dbContext.UserRoles.Where(x => !x.IsDeleted && x.UserId == s.Id).Select(p => p.RoleId).ToList(),
                        Organizations = _dbContext.OrganizationUsers.Where(x => !x.IsDeleted && x.UserId == s.Id).Select(p => p.OrganizationId).ToList()
                    });

                    var pagination = PagedList<User>.ToPagedList(queryable, pagingParameter.PageNumber, pagingParameter.PageSize);

                    result.SetData(new PagingResult<PagedList<User>>()
                    {
                        Items = pagination,
                        TotalCount = pagination.TotalCount,
                    });

                    result.SetMessage("İşlem başarı ile gerçekleşti.");
                }
                catch (Exception ex)
                {
                    result.SetIsSuccess(false);
                    result.SetMessage(ex.Message);
                }
            }

            return result;
        }

        public async Task<Result<List<User>>> GetUsers()
        {
            var result = new Result<List<User>>();
            
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var data = await _dbContext.Users.Where(x => !x.IsDeleted).ToListAsync();

                    result.SetData(data);
                    result.SetMessage("İşlem başarı ile gerçekleşti.");
                }
                catch (Exception ex)
                {
                    result.SetIsSuccess(false);
                    result.SetMessage(ex.Message);
                }
            }

            return result;
        }

        public async Task<Result<User>> Save(User user)
        {
            var result = new Result<User>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {

                    if(!_dbContext.Users.Where(x => x.Username == user.Username).Any())
                    {
                        var hashedPassword = HashPasword(user.Password, out var salt);

                        user.Password = hashedPassword;
                        user.Salt = salt;

                        _dbContext.Users.Add(user);
                        await _dbContext.SaveChangesAsync();

                        foreach (var role in user.Roles)
                        {
                            UserRole ur = new UserRole();
                            ur.RoleId = role;
                            ur.UserId = user.Id;
                            ur.IsDeleted = false;

                            _dbContext.Add(ur);
                            await _dbContext.SaveChangesAsync();

                            var rolePerms = await _dbContext.RolePermissions.Include(x => x.Permission).Where(x => x.RoleId == role).Select(s => s.Permission).ToListAsync();
                            user.Permissions.AddRange(rolePerms);
                        }

                        foreach (var permission in user.Permissions)
                        {
                            UserPermission up = new UserPermission();
                            up.PermissionId = permission.Id;
                            up.UserId = user.Id;
                            up.IsDeleted = false;

                            _dbContext.Add(up);
                            await _dbContext.SaveChangesAsync();
                        }

                        foreach (var organization in user.Organizations)
                        {
                            OrganizationUser ou = new OrganizationUser();
                            ou.OrganizationId = organization;
                            ou.UserId = user.Id;
                            ou.IsDeleted = false;

                            _dbContext.Add(ou);
                            await _dbContext.SaveChangesAsync();
                        }

                        transaction.Commit();

                        result.SetData(user);
                        result.SetMessage("İşlem başarı ile gerçekleşti.");
                    }
                    else
                    {
                        result.SetIsSuccess(false);
                        result.SetMessage("Aynı kullanıcı ismine sahip başka bir kullanıcı bulunmaktadır.");
                    }
                    
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    result.SetIsSuccess(false);
                    result.SetMessage(ex.Message);
                }
            }

            return result;
        }

        public async Task<Result<User>> Update(User user)
        {
            var result = new Result<User>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldUser = await _dbContext.Users.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
                    if(oldUser != null) 
                    { 
                        oldUser.Surname = user.Surname;
                        oldUser.Name = user.Name;
                        oldUser.Email = user.Email;
                        oldUser.Phone = user.Phone;
                        oldUser.Username = user.Username;

                        var roles = await _dbContext.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();
                        _dbContext.UserRoles.RemoveRange(roles);

                        var permissions = await _dbContext.UserPermissions.Where(x => x.UserId == user.Id).ToListAsync();
                        _dbContext.UserPermissions.RemoveRange(permissions);

                        var organizations = await _dbContext.OrganizationUsers.Where(x => x.UserId == user.Id).ToListAsync();
                        _dbContext.OrganizationUsers.RemoveRange(organizations);
                        await _dbContext.SaveChangesAsync();

                        foreach (var role in user.Roles)
                        {
                            UserRole ur = new UserRole();
                            ur.RoleId = role;
                            ur.UserId = user.Id;
                            ur.IsDeleted = false;

                            _dbContext.Add(ur);
                            await _dbContext.SaveChangesAsync();

                            var rolePerms = await _dbContext.RolePermissions.Include(x => x.Permission).Where(x => x.RoleId == role).Select(s => s.Permission).ToListAsync();
                            user.Permissions.AddRange(rolePerms);
                        }

                        foreach (var permission in user.Permissions)
                        {
                            UserPermission up = new UserPermission();
                            up.PermissionId = permission.Id;
                            up.UserId = user.Id;
                            up.IsDeleted = false;

                            _dbContext.Add(up);
                            await _dbContext.SaveChangesAsync();
                        }

                        foreach (var organization in user.Organizations)
                        {
                            OrganizationUser ou = new OrganizationUser();
                            ou.OrganizationId = organization;
                            ou.UserId = user.Id;
                            ou.IsDeleted = false;

                            _dbContext.Add(ou);
                            await _dbContext.SaveChangesAsync();
                        }

                        transaction.Commit();

                        result.SetData(user);
                        result.SetMessage("İşlem başarı ile gerçekleşti.");
                    }
                    else
                    {
                        result.SetIsSuccess(false);
                        result.SetMessage("Böyle bir kayıt bulunmamaktadır.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    result.SetIsSuccess(false);
                    result.SetMessage(ex.Message);
                }
            }

            return result;
        }

        public async Task<Result<User>> Delete(long id)
        {
            var result = new Result<User>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldUser = await _dbContext.Users.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
                    if (oldUser != null)
                    {
                        oldUser.IsDeleted = true;

                        var roles = await _dbContext.UserRoles.Where(x => x.UserId == oldUser.Id).ToListAsync();
                        _dbContext.UserRoles.RemoveRange(roles);

                        var permissions = await _dbContext.UserPermissions.Where(x => x.UserId == oldUser.Id).ToListAsync();
                        _dbContext.UserPermissions.RemoveRange(permissions);

                        var organizations = await _dbContext.OrganizationUsers.Where(x => x.UserId == oldUser.Id).ToListAsync();
                        _dbContext.OrganizationUsers.RemoveRange(organizations);

                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(oldUser);
                        result.SetMessage("İşlem başarı ile gerçekleşti.");
                    }
                    else
                    {
                        result.SetIsSuccess(false);
                        result.SetMessage("Böyle bir kayıt bulunmamaktadır.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    result.SetIsSuccess(false);
                    result.SetMessage(ex.Message);
                }
            }

            return result;
        }

        public async Task<Result<User>> GetById(long id)
        {
            var result = new Result<User>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var user = await _dbContext.Users.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        user.Password = null;
                        user.Salt = null;
                        user.Roles = await _dbContext.UserRoles.Where(x => x.UserId == id && !x.IsDeleted).Select(s => s.RoleId).ToListAsync();
                        user.Organizations = await _dbContext.OrganizationUsers.Where(x => x.UserId == id && !x.IsDeleted).Select(s => s.OrganizationId).ToListAsync();

                        result.SetData(user);
                        result.SetMessage("İşlem başarı ile gerçekleşti.");
                    }
                    else
                    {
                        result.SetIsSuccess(false);
                        result.SetMessage("Böyle bir kayıt bulunmamaktadır.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    result.SetIsSuccess(false);
                    result.SetMessage(ex.Message);
                }
            }

            return result;
        }

        public async Task<Result<List<String>>> GetUserPermissions(string token)
        {
            var result = new Result<List<String>>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var principal = await GetPrincipalFromToken(token);
                    var userId = principal.Claims.Where(x => x.Type == "id").Select(s => s.Value).FirstOrDefault();

                    var roleIds = await _dbContext.UserRoles.Include(x => x.Role).Where(x => !x.Role.IsDeleted && x.UserId == Convert.ToInt64(userId)).Select(s => s.RoleId).ToListAsync();

                    var permissions = await _dbContext.RolePermissions.Include(x => x.Permission).Where(x => !x.Permission.IsDeleted && roleIds.Contains(x.RoleId))
                                                            .Select(s => s.Permission.Code).ToListAsync();

                    result.SetData(permissions);
                    result.SetMessage("Islem basari ile gerceklesti.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    result.SetIsSuccess(false);
                    result.SetMessage(ex.Message);
                }
            }

            return result;
        }

        private Task<ClaimsPrincipal?> GetPrincipalFromToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return Task.FromResult(principal);

        }

        private string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }

    }
}

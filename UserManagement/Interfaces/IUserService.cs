using Microsoft.AspNetCore.Mvc;
using UserManagement.Entity;
using UserManagement.Model;

namespace UserManagement.Interfaces
{
    public interface IUserService
    {
        Task<Result<PagingResult<PagedList<User>>>> Paginate(PagingParameter pagingParameter);
        Task<Result<List<User>>> GetUsers();
        Task<Result<User>> Save(User user);
        Task<Result<User>> Update(User user);
        Task<Result<User>> Delete(long id);
        Task<Result<User>> GetById(long id, string token);
        Task<Result<User>> UserAvatarUpdate(long id, long fileId);
        Task<Result<List<String>>> GetUserPermissions(string token);

    }
}

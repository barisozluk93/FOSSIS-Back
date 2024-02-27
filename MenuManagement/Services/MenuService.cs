using Microsoft.EntityFrameworkCore;
using MenuManagement.DbContexts;
using MenuManagement.Entity;
using MenuManagement.Interfaces;
using MenuManagement.Model;
using System.Data;

namespace MenuManagement.Services
{
    public class MenuService : IMenuService
    {
        private readonly MenuManagementContext _dbContext;

        public MenuService(MenuManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PagingResult<PagedList<Menu>>>> Paginate(PagingParameter pagingParameter)
        {
            var result = new Result<PagingResult<PagedList<Menu>>>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var queryable = _dbContext.Menu.Include(x => x.Parent);
                    var pagination = PagedList<Menu>.ToPagedList(queryable, pagingParameter.PageNumber, pagingParameter.PageSize);

                    result.SetData(new PagingResult<PagedList<Menu>>()
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

        public async Task<Result<List<Menu>>> GetMenus()
        {
            var result = new Result<List<Menu>>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var data = await _dbContext.Menu.Where(x => !x.IsDeleted).ToListAsync();

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

        public async Task<Result<List<Menu>>> GetMenuList()
        {
            var result = new Result<List<Menu>>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var rootMenus = await _dbContext.Menu
                        .Where(x => !x.IsDeleted && x.ParentId == null)
                        .ToListAsync();

                    foreach (var rootMenu in rootMenus) 
                    {
                        rootMenu.ChildMenus = await GetChildMenus(rootMenu.Id);
                    }

                    result.SetData(rootMenus.OrderBy(o => o.Id).ToList());
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

        private async Task<List<Menu>> GetChildMenus(long parentId)
        {
            var childs = await _dbContext.Menu.Where(x => x.ParentId == parentId && !x.IsDeleted).ToListAsync();

            foreach (var child in childs)
            {
                child.ChildMenus = await GetChildMenus(child.Id);
            }

            return childs;
        }

        public async Task<Result<Menu>> Save(Menu menu)
        {
            var result = new Result<Menu>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    if (!_dbContext.Menu.Where(x => (x.Name == menu.Name) && !x.IsDeleted).Any())
                    {
                        _dbContext.Add(menu);
                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(menu);
                        result.SetMessage("İşlem başarı ile gerçekleşti.");
                    }
                    else
                    {
                        result.SetIsSuccess(false);
                        result.SetMessage("Aynı isim ile tanımlı bir menü bileşeni bulunmaktadır.");
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

        public async Task<Result<Menu>> Update(Menu updatedMenu)
        {
            var result = new Result<Menu>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var existingMenu = await _dbContext.Menu
                        .Where(x => x.Id == updatedMenu.Id && !x.IsDeleted)
                        .FirstOrDefaultAsync();

                    if (existingMenu != null)
                    {
                        existingMenu.Name = updatedMenu.Name;
                        existingMenu.ParentId = updatedMenu.ParentId;
                        existingMenu.NameEn = updatedMenu.NameEn;
                        existingMenu.Url = updatedMenu.Url;
                        existingMenu.Icon = updatedMenu.Icon;
                        existingMenu.PermissionId = updatedMenu.PermissionId;


                        await _dbContext.SaveChangesAsync();

                        transaction.Commit();

                        result.SetData(existingMenu);
                        result.SetMessage("İşlem başarı ile gerçekleşti.");
                    }
                    else
                    {
                        result.SetIsSuccess(false);
                        result.SetMessage("Böyle bir kayıt bulunmamaktadır veya silinmiş bir kayıt güncellenemez.");
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

        public async Task<Result<Menu>> Delete(long id)
        {
            var result = new Result<Menu>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var menuToDelete = await _dbContext.Menu.FindAsync(id);

                    if (menuToDelete != null && !menuToDelete.IsDeleted)
                    {
                        await DeleteMenuAndSubmenus(menuToDelete);

                        var saveResult = await _dbContext.SaveChangesAsync();

                        if (saveResult > 0)
                        {
                            transaction.Commit();

                            result.SetData(menuToDelete);
                            result.SetMessage("İşlem başarı ile gerçekleşti.");
                        }
                        else
                        {
                            transaction.Rollback();

                            result.SetIsSuccess(false);
                            result.SetMessage("İşlem başarısız oldu. Değişiklikler kaydedilemedi.");
                        }
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

        private async Task DeleteMenuAndSubmenus(Menu menu)
        {
            menu.IsDeleted = true;

            var submenus = await _dbContext.Menu.Where(x => x.ParentId == menu.Id && !x.IsDeleted).ToListAsync();

            foreach (var submenu in submenus)
            {
                await DeleteMenuAndSubmenus(submenu);
            }
        }

        public async Task<Result<Menu>> GetById(long id)
        {
            var result = new Result<Menu>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var menu = await _dbContext.Menu.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
                    if (menu != null)
                    {
                        result.SetData(menu);
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
                }
            }

            return result;
        }
    }
}

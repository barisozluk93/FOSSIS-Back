using MenuManagement.Entity;
using MenuManagement.Model;

namespace MenuManagement.Interfaces
{
    public interface IMenuService
    {
        Task<Result<PagingResult<PagedList<Menu>>>> Paginate(PagingParameter pagingParameter);
        Task<Result<List<Menu>>> GetMenus();
        Task<Result<List<Menu>>> GetMenuList();
        Task<Result<Menu>> Save(Menu menu);
        Task<Result<Menu>> Update(Menu menu);
        Task<Result<Menu>> Delete(long id);

        Task<Result<Menu>> GetById(long id);
    }
}

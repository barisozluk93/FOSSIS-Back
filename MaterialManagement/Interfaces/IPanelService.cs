

using MaterialManagement.Entity;
using MaterialManagement.Model;

namespace MaterialManagement.Interfaces
{
    public interface IPanelService
    {
        Task<Result<PagingResult<PagedList<Panel>>>> Paginate(PagingParameter pagingParameter);
        Task<Result<List<Panel>>> GetPanels();
        Task<Result<Panel>> Save(Panel panel);
        Task<Result<Panel>> Update(Panel panel);
        Task<Result<Panel>> Delete(long id);
        Task<Result<Panel>> GetById(long id);

    }
}

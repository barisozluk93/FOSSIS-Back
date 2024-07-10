

using MaterialManagement.Entity;
using MaterialManagement.Model;

namespace MaterialManagement.Interfaces
{
    public interface ICableService
    {
        Task<Result<PagingResult<PagedList<Cable>>>> Paginate(PagingParameter pagingParameter);
        Task<Result<List<Cable>>> GetCables();
        Task<Result<Cable>> Save(Cable cable);
        Task<Result<Cable>> Update(Cable cable);
        Task<Result<Cable>> Delete(long id);
        Task<Result<Cable>> GetById(long id);

    }
}

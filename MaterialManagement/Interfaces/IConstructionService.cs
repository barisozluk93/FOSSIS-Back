

using MaterialManagement.Entity;
using MaterialManagement.Model;

namespace MaterialManagement.Interfaces
{
    public interface IConstructionService
    {
        Task<Result<PagingResult<PagedList<Construction>>>> Paginate(PagingParameter pagingParameter);
        Task<Result<List<Construction>>> GetConstructions();
        Task<Result<Construction>> Save(Construction construction);
        Task<Result<Construction>> Update(Construction construction);
        Task<Result<Construction>> Delete(long id);
        Task<Result<Construction>> GetById(long id);

    }
}

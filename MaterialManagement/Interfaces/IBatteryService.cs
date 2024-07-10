

using MaterialManagement.Entity;
using MaterialManagement.Model;

namespace MaterialManagement.Interfaces
{
    public interface IBatteryService
    {
        Task<Result<PagingResult<PagedList<Battery>>>> Paginate(PagingParameter pagingParameter);
        Task<Result<List<Battery>>> GetBatteries();
        Task<Result<Battery>> Save(Battery battery);
        Task<Result<Battery>> Update(Battery battery);
        Task<Result<Battery>> Delete(long id);
        Task<Result<Battery>> GetById(long id);

    }
}

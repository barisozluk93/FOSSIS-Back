

using MaterialManagement.Entity;
using MaterialManagement.Model;

namespace MaterialManagement.Interfaces
{
    public interface IHeatPumpService
    {
        Task<Result<PagingResult<PagedList<HeatPump>>>> Paginate(PagingParameter pagingParameter);
        Task<Result<List<HeatPump>>> GetHeatPumps();
        Task<Result<HeatPump>> Save(HeatPump heatPump);
        Task<Result<HeatPump>> Update(HeatPump heatPump);
        Task<Result<HeatPump>> Delete(long id);
        Task<Result<HeatPump>> GetById(long id);

    }
}

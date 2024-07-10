

using MaterialManagement.Entity;
using MaterialManagement.Model;

namespace MaterialManagement.Interfaces
{
    public interface IChargingStationService
    {
        Task<Result<PagingResult<PagedList<ChargingStation>>>> Paginate(PagingParameter pagingParameter);
        Task<Result<List<ChargingStation>>> GetChargingStations();
        Task<Result<ChargingStation>> Save(ChargingStation chargingStation);
        Task<Result<ChargingStation>> Update(ChargingStation chargingStation);
        Task<Result<ChargingStation>> Delete(long id);
        Task<Result<ChargingStation>> GetById(long id);

    }
}

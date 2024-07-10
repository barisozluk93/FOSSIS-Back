

using MaterialManagement.Entity;
using MaterialManagement.Model;

namespace MaterialManagement.Interfaces
{
    public interface IInverterService
    {
        Task<Result<PagingResult<PagedList<Inverter>>>> Paginate(PagingParameter pagingParameter);
        Task<Result<List<Inverter>>> GetInverters();
        Task<Result<Inverter>> Save(Inverter inverter);
        Task<Result<Inverter>> Update(Inverter inverter);
        Task<Result<Inverter>> Delete(long id);
        Task<Result<Inverter>> GetById(long id);

    }
}

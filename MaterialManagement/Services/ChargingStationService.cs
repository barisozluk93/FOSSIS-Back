using MaterialManagement.DbContexts;
using MaterialManagement.Entity;
using MaterialManagement.Interfaces;
using MaterialManagement.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace MaterialManagement.Services
{
    public class ChargingStationService : IChargingStationService
    { 
        private readonly MaterialManagementContext _dbContext;

        public ChargingStationService(MaterialManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PagingResult<PagedList<ChargingStation>>>> Paginate(PagingParameter pagingParameter)
        {
            var result = new Result<PagingResult<PagedList<ChargingStation>>>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var queryable = _dbContext.ChargingStations;
                    var pagination = PagedList<ChargingStation>.ToPagedList(queryable, pagingParameter.PageNumber, pagingParameter.PageSize);

                    result.SetData(new PagingResult<PagedList<ChargingStation>> ()
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

        public async Task<Result<List<ChargingStation>>> GetChargingStations()
        {
            var result = new Result<List<ChargingStation>>();
            
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var data = await _dbContext.ChargingStations.Where(x => !x.IsDeleted).ToListAsync();

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

        public async Task<Result<ChargingStation>> Save(ChargingStation chargingStation)
        {
            var result = new Result<ChargingStation>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    if (!_dbContext.ChargingStations.Where(x => (x.Model == chargingStation.Model) && !x.IsDeleted).Any())
                    {
                        _dbContext.Add(chargingStation);
                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(chargingStation);
                        result.SetMessage("İşlem başarı ile gerçekleşti.");
                    }
                    else
                    {
                        result.SetIsSuccess(false);
                        result.SetMessage("Aynı isim veya kodla tanımlı bir yetki bulunmaktadır.");
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

        public async Task<Result<ChargingStation>> Update(ChargingStation chargingStation)
        {
            var result = new Result<ChargingStation>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldChargingStation = await _dbContext.ChargingStations.Where(x => x.Id == chargingStation.Id && !x.IsDeleted).FirstOrDefaultAsync();

                    if (oldChargingStation != null)
                    {
                        if (!_dbContext.ChargingStations.Where(x => x.Id != oldChargingStation.Id && (x.Model == chargingStation.Model) && !x.IsDeleted).Any())
                        {
                            oldChargingStation.Model = chargingStation.Model;
                            oldChargingStation.Manufacturer = chargingStation.Manufacturer;
                            oldChargingStation.Power = chargingStation.Power;
                            oldChargingStation.Series = chargingStation.Series;
                            oldChargingStation.Type = chargingStation.Type;

                            await _dbContext.SaveChangesAsync();
                            transaction.Commit();

                            result.SetData(chargingStation);
                            result.SetMessage("İşlem başarı ile gerçekleşti.");
                        }
                        else
                        {
                            result.SetIsSuccess(false);
                            result.SetMessage("Aynı isim veya kodla tanımlı bir yetki bulunmaktadır.");
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

        public async Task<Result<ChargingStation>> Delete(long id)
        {
            var result = new Result<ChargingStation>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldChargingStation = await _dbContext.ChargingStations.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
                    if (oldChargingStation != null)
                    {
                        oldChargingStation.IsDeleted = true;

                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(oldChargingStation);
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

                    result.SetIsSuccess(false);
                    result.SetMessage(ex.Message);
                }
            }

            return result;
        }

        public async Task<Result<ChargingStation>> GetById(long id)
        {
            var result = new Result<ChargingStation>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var permission = await _dbContext.ChargingStations.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
                    if (permission != null)
                    {
                        result.SetData(permission);
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

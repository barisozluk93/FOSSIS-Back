using MaterialManagement.DbContexts;
using MaterialManagement.Entity;
using MaterialManagement.Interfaces;
using MaterialManagement.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace MaterialManagement.Services
{
    public class HeatPumpService : IHeatPumpService
    { 
        private readonly MaterialManagementContext _dbContext;

        public HeatPumpService(MaterialManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PagingResult<PagedList<HeatPump>>>> Paginate(PagingParameter pagingParameter)
        {
            var result = new Result<PagingResult<PagedList<HeatPump>>>();
            string lowerFilterText = string.IsNullOrEmpty(pagingParameter.FilterText) ? null : pagingParameter.FilterText.ToLower();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var queryable = _dbContext.HeatPumps.Where(x => (String.IsNullOrEmpty(lowerFilterText) || (x.Manufacturer.ToLower().Contains(lowerFilterText)))); ;
                    var pagination = PagedList<HeatPump>.ToPagedList(queryable, pagingParameter.PageNumber, pagingParameter.PageSize);

                    result.SetData(new PagingResult<PagedList<HeatPump>> ()
                    {
                        Items = pagination,
                        TotalCount = pagination.TotalCount,
                        TotalPages = pagination.TotalPages
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

        public async Task<Result<List<HeatPump>>> GetHeatPumps()
        {
            var result = new Result<List<HeatPump>>();
            
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var data = await _dbContext.HeatPumps.Where(x => !x.IsDeleted).ToListAsync();

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

        public async Task<Result<HeatPump>> Save(HeatPump heatPump)
        {
            var result = new Result<HeatPump>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    if (!_dbContext.HeatPumps.Where(x => (x.Model == heatPump.Model) && !x.IsDeleted).Any())
                    {
                        _dbContext.Add(heatPump);
                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(heatPump);
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

        public async Task<Result<HeatPump>> Update(HeatPump heatPump)
        {
            var result = new Result<HeatPump>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldHeatPump = await _dbContext.HeatPumps.Where(x => x.Id == heatPump.Id && !x.IsDeleted).FirstOrDefaultAsync();

                    if (oldHeatPump != null)
                    {
                        if (!_dbContext.HeatPumps.Where(x => x.Id != oldHeatPump.Id && (x.Model == heatPump.Model) && !x.IsDeleted).Any())
                        {
                            oldHeatPump.Model = heatPump.Model;
                            oldHeatPump.Manufacturer = heatPump.Manufacturer;
                            oldHeatPump.StructureType = heatPump.StructureType;
                            oldHeatPump.NominalCapacity = heatPump.NominalCapacity;
                            oldHeatPump.Type = heatPump.Type;
                            oldHeatPump.COP = heatPump.COP;

                            await _dbContext.SaveChangesAsync();
                            transaction.Commit();

                            result.SetData(heatPump);
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

        public async Task<Result<HeatPump>> Delete(long id)
        {
            var result = new Result<HeatPump>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldHeatPump = await _dbContext.HeatPumps.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
                    if (oldHeatPump != null)
                    {
                        oldHeatPump.IsDeleted = true;

                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(oldHeatPump);
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

        public async Task<Result<HeatPump>> GetById(long id)
        {
            var result = new Result<HeatPump>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var permission = await _dbContext.HeatPumps.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
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

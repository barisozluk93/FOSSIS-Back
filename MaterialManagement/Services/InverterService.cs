using MaterialManagement.DbContexts;
using MaterialManagement.Entity;
using MaterialManagement.Interfaces;
using MaterialManagement.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace MaterialManagement.Services
{
    public class InverterService : IInverterService
    { 
        private readonly MaterialManagementContext _dbContext;

        public InverterService(MaterialManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PagingResult<PagedList<Inverter>>>> Paginate(PagingParameter pagingParameter)
        {
            var result = new Result<PagingResult<PagedList<Inverter>>>();
            string lowerFilterText = string.IsNullOrEmpty(pagingParameter.FilterText) ? null : pagingParameter.FilterText.ToLower();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var queryable = _dbContext.Inverters.Where(x => (String.IsNullOrEmpty(lowerFilterText) || (x.Manufacturer.ToLower().Contains(lowerFilterText)))); ;
                    var pagination = PagedList<Inverter>.ToPagedList(queryable, pagingParameter.PageNumber, pagingParameter.PageSize);

                    result.SetData(new PagingResult<PagedList<Inverter>> ()
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

        public async Task<Result<List<Inverter>>> GetInverters()
        {
            var result = new Result<List<Inverter>>();
            
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var data = await _dbContext.Inverters.Where(x => !x.IsDeleted).ToListAsync();

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

        public async Task<Result<Inverter>> Save(Inverter inverter)
        {
            var result = new Result<Inverter>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    if (!_dbContext.Inverters.Where(x => (x.Model == inverter.Model) && !x.IsDeleted).Any())
                    {
                        _dbContext.Add(inverter);
                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(inverter);
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

        public async Task<Result<Inverter>> Update(Inverter inverter)
        {
            var result = new Result<Inverter>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldInverter = await _dbContext.Inverters.Where(x => x.Id == inverter.Id && !x.IsDeleted).FirstOrDefaultAsync();

                    if (oldInverter != null)
                    {
                        if (!_dbContext.Inverters.Where(x => x.Id != oldInverter.Id && (x.Model == inverter.Model) && !x.IsDeleted).Any())
                        {
                            oldInverter.Model = inverter.Model;
                            oldInverter.Manufacturer = inverter.Manufacturer;
                            oldInverter.NominalACPower = inverter.NominalACPower;
                            oldInverter.Series = inverter.Series;
                            oldInverter.Type = inverter.Type;

                            await _dbContext.SaveChangesAsync();
                            transaction.Commit();

                            result.SetData(inverter);
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

        public async Task<Result<Inverter>> Delete(long id)
        {
            var result = new Result<Inverter>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldInverter = await _dbContext.Inverters.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
                    if (oldInverter != null)
                    {
                        oldInverter.IsDeleted = true;

                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(oldInverter);
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

        public async Task<Result<Inverter>> GetById(long id)
        {
            var result = new Result<Inverter>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var permission = await _dbContext.Inverters.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
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

using MaterialManagement.DbContexts;
using MaterialManagement.Entity;
using MaterialManagement.Interfaces;
using MaterialManagement.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace MaterialManagement.Services
{
    public class CableService : ICableService
    { 
        private readonly MaterialManagementContext _dbContext;

        public CableService(MaterialManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PagingResult<PagedList<Cable>>>> Paginate(PagingParameter pagingParameter)
        {
            var result = new Result<PagingResult<PagedList<Cable>>>();
            string lowerFilterText = string.IsNullOrEmpty(pagingParameter.FilterText) ? null : pagingParameter.FilterText.ToLower();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var queryable = _dbContext.Cables.Where(x => (String.IsNullOrEmpty(lowerFilterText) || (x.Manufacturer.ToLower().Contains(lowerFilterText)))); ;
                    var pagination = PagedList<Cable>.ToPagedList(queryable, pagingParameter.PageNumber, pagingParameter.PageSize);

                    result.SetData(new PagingResult<PagedList<Cable>> ()
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

        public async Task<Result<List<Cable>>> GetCables()
        {
            var result = new Result<List<Cable>>();
            
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var data = await _dbContext.Cables.Where(x => !x.IsDeleted).ToListAsync();

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

        public async Task<Result<Cable>> Save(Cable cable)
        {
            var result = new Result<Cable>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    if (!_dbContext.Cables.Where(x => (x.Model == cable.Model) && !x.IsDeleted).Any())
                    {
                        _dbContext.Add(cable);
                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(cable);
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

        public async Task<Result<Cable>> Update(Cable cable)
        {
            var result = new Result<Cable>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldCable = await _dbContext.Cables.Where(x => x.Id == cable.Id && !x.IsDeleted).FirstOrDefaultAsync();

                    if (oldCable != null)
                    {
                        if (!_dbContext.Cables.Where(x => x.Id != oldCable.Id && (x.Model == cable.Model) && !x.IsDeleted).Any())
                        {
                            oldCable.Model = cable.Model;
                            oldCable.Manufacturer = cable.Manufacturer;
                            oldCable.Series = cable.Series;
                            oldCable.Type = cable.Type;

                            await _dbContext.SaveChangesAsync();
                            transaction.Commit();

                            result.SetData(cable);
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

        public async Task<Result<Cable>> Delete(long id)
        {
            var result = new Result<Cable>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldCable = await _dbContext.Cables.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
                    if (oldCable != null)
                    {
                        oldCable.IsDeleted = true;

                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(oldCable);
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

        public async Task<Result<Cable>> GetById(long id)
        {
            var result = new Result<Cable>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var permission = await _dbContext.Cables.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
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

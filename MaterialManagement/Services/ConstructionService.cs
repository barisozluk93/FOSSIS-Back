using MaterialManagement.DbContexts;
using MaterialManagement.Entity;
using MaterialManagement.Interfaces;
using MaterialManagement.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace MaterialManagement.Services
{
    public class ConstructionService : IConstructionService
    { 
        private readonly MaterialManagementContext _dbContext;

        public ConstructionService(MaterialManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PagingResult<PagedList<Construction>>>> Paginate(PagingParameter pagingParameter)
        {
            var result = new Result<PagingResult<PagedList<Construction>>>();
            string lowerFilterText = string.IsNullOrEmpty(pagingParameter.FilterText) ? null : pagingParameter.FilterText.ToLower();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var queryable = _dbContext.Constructions.Where(x => (String.IsNullOrEmpty(lowerFilterText) || (x.Manufacturer.ToLower().Contains(lowerFilterText)))); ;
                    var pagination = PagedList<Construction>.ToPagedList(queryable, pagingParameter.PageNumber, pagingParameter.PageSize);

                    result.SetData(new PagingResult<PagedList<Construction>> ()
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

        public async Task<Result<List<Construction>>> GetConstructions()
        {
            var result = new Result<List<Construction>>();
            
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var data = await _dbContext.Constructions.Where(x => !x.IsDeleted).ToListAsync();

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

        public async Task<Result<Construction>> Save(Construction construction)
        {
            var result = new Result<Construction>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    if (!_dbContext.Constructions.Where(x => (x.Model == construction.Model) && !x.IsDeleted).Any())
                    {
                        _dbContext.Add(construction);
                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(construction);
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

        public async Task<Result<Construction>> Update(Construction construction)
        {
            var result = new Result<Construction>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldConstruction = await _dbContext.Constructions.Where(x => x.Id == construction.Id && !x.IsDeleted).FirstOrDefaultAsync();

                    if (oldConstruction != null)
                    {
                        if (!_dbContext.Constructions.Where(x => x.Id != oldConstruction.Id && (x.Model == construction.Model) && !x.IsDeleted).Any())
                        {
                            oldConstruction.Model = construction.Model;
                            oldConstruction.Manufacturer = construction.Manufacturer;
                            oldConstruction.PanelOrientation = construction.PanelOrientation;
                            oldConstruction.Series = construction.Series;
                            oldConstruction.Type = construction.Type;

                            await _dbContext.SaveChangesAsync();
                            transaction.Commit();

                            result.SetData(construction);
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

        public async Task<Result<Construction>> Delete(long id)
        {
            var result = new Result<Construction>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldConstruction = await _dbContext.Constructions.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
                    if (oldConstruction != null)
                    {
                        oldConstruction.IsDeleted = true;

                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(oldConstruction);
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

        public async Task<Result<Construction>> GetById(long id)
        {
            var result = new Result<Construction>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var permission = await _dbContext.Constructions.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
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

using MaterialManagement.DbContexts;
using MaterialManagement.Entity;
using MaterialManagement.Interfaces;
using MaterialManagement.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace MaterialManagement.Services
{
    public class PanelService : IPanelService
    { 
        private readonly MaterialManagementContext _dbContext;

        public PanelService(MaterialManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PagingResult<PagedList<Panel>>>> Paginate(PagingParameter pagingParameter)
        {
            var result = new Result<PagingResult<PagedList<Panel>>>();
            string lowerFilterText = string.IsNullOrEmpty(pagingParameter.FilterText) ? null : pagingParameter.FilterText.ToLower();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var queryable = _dbContext.Panels.Where(x => (String.IsNullOrEmpty(lowerFilterText) || (x.Manufacturer.ToLower().Contains(lowerFilterText)))); ;
                    var pagination = PagedList<Panel>.ToPagedList(queryable, pagingParameter.PageNumber, pagingParameter.PageSize);

                    result.SetData(new PagingResult<PagedList<Panel>> ()
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

        public async Task<Result<List<Panel>>> GetPanels()
        {
            var result = new Result<List<Panel>>();
            
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var data = await _dbContext.Panels.Where(x => !x.IsDeleted).ToListAsync();

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

        public async Task<Result<Panel>> Save(Panel panel)
        {
            var result = new Result<Panel>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    if (!_dbContext.Panels.Where(x => (x.Model == panel.Model) && !x.IsDeleted).Any())
                    {
                        _dbContext.Add(panel);
                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(panel);
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

        public async Task<Result<Panel>> Update(Panel panel)
        {
            var result = new Result<Panel>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldPanel = await _dbContext.Panels.Where(x => x.Id == panel.Id && !x.IsDeleted).FirstOrDefaultAsync();

                    if (oldPanel != null)
                    {
                        if (!_dbContext.Panels.Where(x => x.Id != oldPanel.Id && (x.Model == panel.Model) && !x.IsDeleted).Any())
                        {
                            oldPanel.Model = panel.Model;
                            oldPanel.Manufacturer = panel.Manufacturer;
                            oldPanel.MaximumDCPower = panel.MaximumDCPower;
                            oldPanel.Series = panel.Series;
                            oldPanel.Type = panel.Type;

                            await _dbContext.SaveChangesAsync();
                            transaction.Commit();

                            result.SetData(panel);
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

        public async Task<Result<Panel>> Delete(long id)
        {
            var result = new Result<Panel>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldPanel = await _dbContext.Panels.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
                    if (oldPanel != null)
                    {
                        oldPanel.IsDeleted = true;

                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(oldPanel);
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

        public async Task<Result<Panel>> GetById(long id)
        {
            var result = new Result<Panel>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var permission = await _dbContext.Panels.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
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

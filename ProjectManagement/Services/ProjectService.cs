using Microsoft.EntityFrameworkCore;
using ProjectManagement.DbContexts;
using ProjectManagement.Entity;
using ProjectManagement.Interfaces;
using ProjectManagement.Model;
using System.Data;

namespace ProjectManagement.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectManagementContext _dbContext;
        public ProjectService(ProjectManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PagingResult<PagedList<Project>>>> Paginate(PagingParameter pagingParameter, long userId)
        {
            var result = new Result<PagingResult<PagedList<Project>>>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var queryable = _dbContext.Projects.Where(x => x.UserId == userId);
                    var pagination = PagedList<Project>.ToPagedList(queryable, pagingParameter.PageNumber, pagingParameter.PageSize);

                    result.SetData(new PagingResult<PagedList<Project>>()
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

        public async Task<Result<List<Project>>> GetProjects()
        {
            var result = new Result<List<Project>>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var data = await _dbContext.Projects.Where(x => !x.IsDeleted).ToListAsync();

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

        public async Task<Result<Project>> Save(Project project)
        {
            var result = new Result<Project>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    if (!_dbContext.Projects.Where(x => (x.Name == project.Name) && !x.IsDeleted).Any())
                    {
                        _dbContext.Add(project);
                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(project);
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

        public async Task<Result<Project>> Update(Project project)
        {
            var result = new Result<Project>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldProject = await _dbContext.Projects.Where(x => x.Id == project.Id && !x.IsDeleted).FirstOrDefaultAsync();

                    if (oldProject != null)
                    {
                        if (!_dbContext.Projects.Where(x => x.Id != oldProject.Id && (x.Name == project.Name) && !x.IsDeleted).Any())
                        {
                            oldProject.Name = project.Name;
                            oldProject.BuildingId = project.BuildingId;
                            oldProject.Location = project.Location;
                            oldProject.RoofArea = project.RoofArea;
                            oldProject.RoofWkt = project.RoofWkt;
                            oldProject.GridSpace = project.GridSpace;
                            oldProject.Margin = project.Margin;
                            oldProject.PanelId = project.PanelId;

                            await _dbContext.SaveChangesAsync();
                            transaction.Commit();

                            result.SetData(project);
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

        public async Task<Result<Project>> Delete(long id)
        {
            var result = new Result<Project>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldProject = await _dbContext.Projects.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
                    if (oldProject != null)
                    {
                        oldProject.IsDeleted = true;

                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(oldProject);
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

        public async Task<Result<Project>> GetById(long id)
        {
            var result = new Result<Project>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var project = await _dbContext.Projects.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
                    if (project != null)
                    {
                        result.SetData(project);
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





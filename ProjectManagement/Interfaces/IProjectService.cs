using ProjectManagement.Entity;
using ProjectManagement.Model;

namespace ProjectManagement.Interfaces
{
    public interface IProjectService
    {
        Task<Result<PagingResult<PagedList<Project>>>> Paginate(PagingParameter pagingParameter, long userId);
        Task<Result<List<Project>>> GetProjects();
        Task<Result<Project>> Save(Project project);
        Task<Result<Project>> Update(Project project);
        Task<Result<Project>> Delete(long id);
        Task<Result<Project>> GetById(long id);
    }
}

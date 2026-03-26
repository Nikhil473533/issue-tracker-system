using IssueTrackerAPI.Models;
namespace IssueTrackerAPI.Repositories;

public interface IWorkItemRepository
{
    IEnumerable<WorkItem> GetAll();
    WorkItem? GetById(int id);
    void Add(WorkItem workItem);
    void Update(WorkItem workItem);
    void SoftDelete(int id);
    void Save();
    String Restore(int id);

}

using IssueTrackerAPI.Models;
using IssueTrackerAPI.DTO;
namespace IssueTrackerAPI.Services;

public interface IWorkItemService

{
    IEnumerable<WorkItemResponse> GetAll();
    WorkItemResponse? GetById(int id);
    void Add(WorkItemRequest workItem);
    void Update(int id, WorkItemRequest workItem);
    String SoftDelete(int id);
}

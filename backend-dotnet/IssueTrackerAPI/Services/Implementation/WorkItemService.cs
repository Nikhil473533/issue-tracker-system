using IssueTrackerAPI.Models;
using IssueTrackerAPI.Repositories;
using IssueTrackerAPI.DTO;
namespace IssueTrackerAPI.Services;

public class WorkItemService : IWorkItemService
{
    private readonly IWorkItemRepository _repository;

    public WorkItemService(IWorkItemRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<WorkItemResponse> GetAll()
    {
        return _repository.GetAll()
               .Select(w => ToResponse(w))
               .ToList();
    }

    public WorkItemResponse? GetById(int id)
    {
        var workItem = _repository.GetById(id);

        if (workItem == null)
        {
            return null;
        }

        return ToResponse(workItem);
    }

    public void Add(WorkItemRequest request)
    {
        var workItem = new WorkItem 
        { 
           Title = request.Title!,
           Description = request.Description!,
           StatusId = request.StatusId,
           UserId = request.UserId,
           createdAt = DateTime.UtcNow
        };

        _repository.Add(workItem);
        _repository.Save();
    }

    public void Update(int id, WorkItemRequest request)
    {
        var workItem = _repository.GetById(id);

        if (workItem == null)
        {
            throw new Exception("WorkItem with id:" + id + " not found");
        }

        workItem.Title = request.Title!;
        workItem.Description = request.Description!;
        workItem.StatusId = request.StatusId;
        workItem.UserId = request.UserId;

        _repository.Update(workItem);
        _repository.Save();
    }

    public String SoftDelete(int id)
    {

        _repository.SoftDelete(id);
        _repository.Save();

        return "Work Item with Id: " + id + " has been deleted.";
    }

    private WorkItemResponse ToResponse(WorkItem workItem)
    {
        return new WorkItemResponse
        { 
            Id = workItem.Id,
            Title = workItem.Title,
            Description = workItem.Description,
            StatusName = workItem.Status?.Name,
            Username = workItem.User?.Username,
            CreatedAt = workItem.createdAt
        };

    }
}


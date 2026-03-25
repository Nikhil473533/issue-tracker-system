namespace IssueTrackerAPI.DTO;
public class WorkItemRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }

    public int StatusId { get; set; }
    public int UserId { get; set; }
}

using System;
namespace IssueTrackerAPI.DTO;
public class WorkItemResponse
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }

    public string? StatusName { get; set; }
    public string? Username { get; set; }

    public DateTime CreatedAt { get; set; }
}

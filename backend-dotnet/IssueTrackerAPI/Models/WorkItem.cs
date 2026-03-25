using System;
namespace IssueTrackerAPI.Models;

public class WorkItem
{

    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int StatusId { get; set; }
    public Status Status { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime createdAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}

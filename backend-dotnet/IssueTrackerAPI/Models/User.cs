using System;
namespace IssueTrackerAPI.Models;

public class User
{

    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
}

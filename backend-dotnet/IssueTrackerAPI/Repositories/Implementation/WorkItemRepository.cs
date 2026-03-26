using IssueTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
namespace IssueTrackerAPI.Repositories;

public class WorkItemRepository : IWorkItemRepository
{
    private readonly AppDbContext _context;

    public WorkItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<WorkItem> GetAll()
    {
        return _context.WorkItems
               .Include(w => w.Status)
               .Include(w => w.User)
               .ToList();
    }

    public WorkItem? GetById(int id)
    {
        return _context.WorkItems
            .IgnoreQueryFilters()
            .Include(w => w.Status)
            .Include(w => w.User)
            .FirstOrDefault(w => w.Id == id);
    }

    public void Add(WorkItem workItem)
    {
        using var transaction = _context.Database.BeginTransaction();

        _context.WorkItems.Add(workItem);
        _context.SaveChanges(); 

        var after = JsonSerializer.Serialize(ToAuditData(workItem));

        AddAudit("CREATE", workItem.Id, null, after);

        _context.SaveChanges();

        transaction.Commit();
    }

    public void Update(WorkItem workItem)
    {
        var existing = _context.WorkItems
                       .AsNoTracking()
                       .FirstOrDefault(w => w.Id == workItem.Id);

        var before = existing == null ? null : JsonSerializer.Serialize(ToAuditData(existing));

        _context.WorkItems.Update(workItem);

        var after = JsonSerializer.Serialize(ToAuditData(workItem));

        AddAudit(
            "UPDATE",
            workItem.Id,
            before,
            after
            );

        _context.SaveChanges();
    }

    public void SoftDelete(int id)
    {
        var workItem = _context.WorkItems
            .IgnoreQueryFilters()
            .FirstOrDefault(w => w.Id == id);

        if (workItem == null)
            throw new Exception("WorkItem not found");

        var before = JsonSerializer.Serialize(ToAuditData(workItem));

        workItem.IsDeleted = true;

        var after = JsonSerializer.Serialize(ToAuditData(workItem));

        AddAudit(
                "DELETE",
                workItem.Id,
                before,
                after
                );

        _context.SaveChanges();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public String Restore(int id)
    {
        var workItem = _context.WorkItems
                       .IgnoreQueryFilters()
                       .FirstOrDefault(w => w.Id == id);

        if (workItem == null)
        {
            throw new Exception("WorkItem with id: " + id + " not found");
        }

        var before = JsonSerializer.Serialize(ToAuditData(workItem));

        workItem.IsDeleted = false;

        var after = JsonSerializer.Serialize(ToAuditData(workItem));

        AddAudit(
           "RESTORE",
           workItem.Id,
           before,
           after
           );

        _context.SaveChanges();
        return "WorkItem with id: " + id + " was restored.";
    }

    private void AddAudit(string action, long id, string? before, string? after)
    {
        var audit = new AuditLog
        {
            entity_name = "WorkItems",
            entity_id = id,
            action = action,
            before_state = before,
            after_state = after,
            event_time = DateTime.UtcNow,
            audit_version = "1.0"
        };

        _context.AuditLogs.Add(audit);
    }

    private object ToAuditData(WorkItem workItem)
    {
        return new 
        {
          workItem.Id,
          workItem.Title,
          workItem.Description,
          workItem.StatusId,
          workItem.UserId,
          workItem.IsDeleted
        };
    }
}

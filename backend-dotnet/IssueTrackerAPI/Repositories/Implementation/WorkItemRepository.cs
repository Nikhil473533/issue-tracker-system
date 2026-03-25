using IssueTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;
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
        _context.WorkItems.Add(workItem);
    }

    public void Update(WorkItem workItem)
    {
        _context.WorkItems.Update(workItem);
    }

    public void SoftDelete(int id)
    {
        var workItem = _context.WorkItems
            .IgnoreQueryFilters()
            .FirstOrDefault(w => w.Id == id);

        if (workItem == null)
            throw new Exception("WorkItem not found");

        workItem.IsDeleted = true;

        _context.SaveChanges();
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}

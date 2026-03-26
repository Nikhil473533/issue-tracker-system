using IssueTrackerAPI.DTO;
using IssueTrackerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace IssueTrackerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkItemController : ControllerBase
{
    private readonly IWorkItemService _service;

    public WorkItemController(IWorkItemService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _service.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _service.GetById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create([FromBody] WorkItemRequest request)
    {
        _service.Add(request);
        return Ok("WorkItem created successfully.");
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] WorkItemRequest request)
    {
        _service.Update(id, request);
        return Ok("WorkItem was updated successfully.");
    }

    [HttpPatch("restore/{id}")]
    public IActionResult Restore(int id)
    {
        String output = _service.Restore(id);

        return Ok(new ApiResponse
        {
             Message = output,
             Success = true
        });
    }

    [HttpDelete("{id}")]
    public IActionResult SoftDelete(int id)
    {
        String output = _service.SoftDelete(id);

        return Ok(new ApiResponse
        {
            Message = output,
            Success = true
        });
    }
}

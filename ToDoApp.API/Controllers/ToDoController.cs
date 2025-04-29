using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Contracts;
using ToDoApp.Application.Services;

namespace ToDoApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    private readonly IToDoService _service;

    
    public ToDoController(IToDoService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateRequest request)
    {
        if (!ModelState.IsValid) return UnprocessableEntity(ModelState);
        
        var todo = await _service.Create(request.Expiry, request.Title, request.Description);
        CreateResponse response = new CreateResponse(todo.Id, todo.Expiry, todo.Title, todo.Description);
        return Ok(response);
    }
    
    [HttpGet("get")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var todo = await _service.Get(id);
            return Ok(todo);
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateRequest request)
    {
        if (!ModelState.IsValid) return UnprocessableEntity(ModelState);
        
        try
        {
            await _service.Update(request.Id, request.Expiry, request.Title, request.Description, request.PercentCompletion, request.IsCompleted);
            return Ok($"Successfully updated todo with id: {request.Id}");
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.Delete(id);
            return Ok($"Successfully deleted todo with id: {id}");
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
    
    
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllToDo()
    {
        try
        {
            var todos = await _service.GetAll();
            return Ok(todos);
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
    
    [HttpGet("get-incoming")]
    public async Task<IActionResult> GetIncomingToDo(string option)
    {
        try
        {
            var todos = await _service.GetIncoming(option);
            return Ok(todos);
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
    
    [HttpPut("set-completion-percentage")]
    public async Task<IActionResult> SetPercentage(int id, int percentage)
    {
        try
        {
            await _service.SetPercentage(id, percentage);
            return Ok($"Successfully set percentage to {percentage} for todo with id: {id}");
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
    
    [HttpPut("mark-as-done")]
    public async Task<IActionResult> MarkAsDone(int id)
    {
        try
        {
            await _service.MarkAsDone(id);
            return Ok($"Successfully marked as complete todo with id: {id}");
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
}
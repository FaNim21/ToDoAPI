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
    public async Task<IActionResult> Create(CreateRequest request)
    {
        var todo = await _service.Create(request.Expiry, request.Title, request.Description);
        CreateResponse response = new CreateResponse(todo.Id, todo.Expiry, todo.Title, todo.Description);
        return Ok(response);
    }
    
    [HttpGet("get")]
    public IActionResult Get(int id)
    {
        return Ok();
    }

    [HttpPut("update")]
    public IActionResult Update()
    {
        return Ok();
    }
    
    [HttpDelete("delete")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
    
    
    [HttpGet("get-all")]
    public IActionResult GetAllToDo()
    {
        return Ok();
    }
    
    [HttpGet("get-incoming")]
    public IActionResult GetIncomingToDo(string date)
    {
        
        
        return Ok();
    }
    
    [HttpPut("set-completion-percentage")]
    public IActionResult SetPercentage(int id, int percentage)
    {
        return Ok();
    }
    
    [HttpPut("mark-as-done")]
    public IActionResult MarkAsDone(int id)
    {
        return Ok();
    }
}
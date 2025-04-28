using ToDoApp.Application.Contracts;
using ToDoApp.Infrastructure.Repositories;

namespace ToDoApp.Application.Services;

public class ToDoService : IToDoService
{
    private readonly IToDoRepository _repository;

    
    public ToDoService(IToDoRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateResponse> Create(DateTime expiry, string title, string description)
    {
        var todo = await _repository.Create(expiry, title, description);
        
        return new CreateResponse(todo.Id, todo.Expiry, todo. Title, todo. Description);
    }
}
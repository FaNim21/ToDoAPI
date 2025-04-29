using ToDoApp.Application.Contracts;
using ToDoApp.Domain.Entities;
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
        
        return new CreateResponse(todo.Id, todo.Expiry, todo.Title, todo.Description);
    }

    public async Task<GeneralResponse> Get(int id)
    {
        var todo = await _repository.GetById(id);
        if (todo == null)
        {
            throw new NullReferenceException("ToDo not found");
        }
        
        return new GeneralResponse(todo.Expiry, todo.Title, todo.Description, todo.PercentCompletion, todo.IsCompleted);
    }

    public async Task Update(int id, DateTime expiry, string title, string description, int percentCompletion, bool isCompleted)
    {
        var todo = await _repository.GetById(id);
        if (todo == null)
        {
            throw new NullReferenceException("ToDo not found");
        }

        await _repository.Update(todo, expiry, title, description, percentCompletion, isCompleted);
    }

    public async Task Delete(int id)
    {
        var todo = await _repository.GetById(id);
        if (todo == null)
        {
            throw new NullReferenceException("ToDo not found");
        }

        await _repository.Delete(todo);
    }

    public async Task<List<ToDoDto>> GetAll()
    {
        var todos = await _repository.GetAll();
        if (todos == null)
        {
            throw new NullReferenceException("ToDos not found");
        }

        return todos.Select(todo => new ToDoDto(todo.Id, todo.Expiry, todo.Title, todo.Description, todo.PercentCompletion, todo.IsCompleted)).ToList();
    }

    public async Task<List<GeneralResponse>> GetIncoming(string option)
    {
        List<ToDo>? todos = option switch
        {
            "today" => await _repository.GetAllIncomingToday(),
            "tomorrow" => await _repository.GetAllIncomingTomorrow(),
            "week" => await _repository.GetAllIncomingCurrentWeek(),
            _ => null
        };

        if (todos == null)
        {
            throw new NullReferenceException("ToDos not found");
        }

        return todos.Select(todo => new GeneralResponse(todo.Expiry, todo.Title, todo.Description, todo.PercentCompletion, todo.IsCompleted)).ToList();
    }

    public async Task SetPercentage(int id, int percentage)
    {
        var todo = await _repository.GetById(id);
        if (todo == null)
        {
            throw new NullReferenceException("ToDo not found");
        }

        await _repository.SetPercentage(todo, percentage);
    }

    public async Task MarkAsDone(int id)
    {
        var todo = await _repository.GetById(id);
        if (todo == null)
        {
            throw new NullReferenceException("ToDo not found");
        }

        await _repository.MarkAsDone(todo);
    }
}
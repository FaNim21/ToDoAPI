using ToDoApp.Application.Contracts;

namespace ToDoApp.Application.Services;

public interface IToDoService
{
    Task<CreateResponse> Create(DateTime expiry, string title, string description);
}
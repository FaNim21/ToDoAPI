using ToDoApp.Application.Contracts;

namespace ToDoApp.Application.Services;

public interface IToDoService
{
    Task<CreateResponse> Create(DateTime expiry, string title, string description);
    Task<GeneralResponse> Get(int id);
    Task Update(int id, DateTime expiry, string title, string description, int percentCompletion, bool isCompleted);
    Task Delete(int id);
    Task<List<ToDoDto>> GetAll();
    Task<List<GeneralResponse>> GetIncoming(string option);
    Task SetPercentage(int id, int percentage);
    Task MarkAsDone(int id);
}
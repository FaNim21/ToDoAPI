using ToDoApp.Domain.Entities;

namespace ToDoApp.Infrastructure.Repositories;

public interface IToDoRepository
{
    Task<ToDo> Create(DateTime expiry, string title, string description);
    Task<ToDo?> Get(int id);
    Task<bool> Update(int id, DateTime expiry, string title, string description, int percentCompletion, bool isCompleted);
    Task<bool> Delete(int id);
    Task<List<ToDo>?> GetAll();
    Task<List<ToDo>?> GetAllIncomingToday();
    Task<List<ToDo>?> GetAllIncomingTomorrow();
    Task<List<ToDo>?> GetAllIncomingWeek();
    Task<bool> SetPercentage(int id, int percentage);
    Task<bool> MarkAsDone(int id);
}
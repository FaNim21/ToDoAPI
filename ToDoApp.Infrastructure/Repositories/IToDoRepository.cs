using ToDoApp.Domain.Entities;

namespace ToDoApp.Infrastructure.Repositories;

public interface IToDoRepository
{
    Task<ToDo> Create(DateTime expiry, string title, string description);
    Task<ToDo?> GetById(int id);
    Task Update(ToDo todo, DateTime expiry, string title, string description, int percentCompletion, bool isCompleted);
    Task Delete(ToDo todo);
    Task<List<ToDo>?> GetAll();
    Task<List<ToDo>?> GetAllIncomingToday();
    Task<List<ToDo>?> GetAllIncomingTomorrow();
    Task<List<ToDo>?> GetAllIncomingCurrentWeek();
    Task SetPercentage(ToDo todo, int percentage);
    Task MarkAsDone(ToDo todo);
}
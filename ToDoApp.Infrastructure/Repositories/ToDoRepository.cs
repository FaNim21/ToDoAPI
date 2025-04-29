using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;
using ToDoApp.Infrastructure.Data;
using ToDoApp.Infrastructure.Interfaces;

namespace ToDoApp.Infrastructure.Repositories;

public class ToDoRepository : IToDoRepository
{
    private readonly DatabaseContext _dbContext;
    private readonly IDateProvider _dateProvider;

    
    public ToDoRepository(DatabaseContext dbContext, IDateProvider dateProvider)
    {
        _dbContext = dbContext;
        _dateProvider = dateProvider;
    }

    public async Task<ToDo> Create(DateTime expiry, string title, string description)
    {
        ToDo todo = new()
        {
            Expiry = expiry,
            Title = title,
            Description = description
        };
        
        await _dbContext.ToDos.AddAsync(todo);
        await _dbContext.SaveChangesAsync();

        return todo;
    }

    public async Task<ToDo?> GetById(int id)
    {
        return await _dbContext.ToDos
            .Where(todo => todo.Id == id).FirstOrDefaultAsync();
    }

    public async Task Update(ToDo todo, DateTime expiry, string title, string description, int percentCompletion, bool isCompleted)
    {
        todo.Expiry = expiry;
        todo.Title = title;
        todo.Description = description;
        todo.PercentCompletion = percentCompletion;
        todo.IsCompleted = isCompleted;
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(ToDo todo)
    {
        _dbContext.ToDos.Remove(todo);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<ToDo>?> GetAll()
    {
        return await _dbContext.ToDos.ToListAsync();
    }

    public async Task<List<ToDo>?> GetAllIncomingToday()
    {
        var today = _dateProvider.Today;

        return await _dbContext.ToDos
            .Where(todo => todo.Expiry >= today && todo.Expiry < today.AddDays(1)).ToListAsync();
    }

    public async Task<List<ToDo>?> GetAllIncomingTomorrow()
    {
        var tomorrow = _dateProvider.Today.AddDays(1);

        return await _dbContext.ToDos
            .Where(todo => todo.Expiry >= tomorrow && todo.Expiry < tomorrow.AddDays(1)).ToListAsync();
    }

    public async Task<List<ToDo>?> GetAllIncomingCurrentWeek()
    {
        var today = _dateProvider.Today;
        int daysSinceMonday = today.DayOfWeek - DayOfWeek.Monday;
        if (daysSinceMonday < 0) daysSinceMonday += 7;
        var monday = today.AddDays(-daysSinceMonday);

        return await _dbContext.ToDos
            .Where(todo => todo.Expiry >= monday && todo.Expiry < monday.AddDays(7))
            .OrderBy(todo => todo.Expiry).ToListAsync();
    }

    public async Task SetPercentage(ToDo todo, int percentage)
    {
        todo.PercentCompletion = percentage;
        await _dbContext.SaveChangesAsync();
    }

    public async Task MarkAsDone(ToDo todo)
    {
        todo.IsCompleted = true;
        await _dbContext.SaveChangesAsync();
    }
}
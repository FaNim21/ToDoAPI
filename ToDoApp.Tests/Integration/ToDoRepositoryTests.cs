using Microsoft.EntityFrameworkCore;
using ToDoApp.Infrastructure.Data;
using ToDoApp.Infrastructure.Interfaces;
using ToDoApp.Infrastructure.Repositories;
using ToDoApp.Tests.Helpers;

namespace ToDoApp.Tests.Integration;

public class ToDoRepositoryTests
{
    private readonly DatabaseContext _context;
    private readonly IToDoRepository _repository;
    private readonly IDateProvider _dateProvider;

    
    public ToDoRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        
        _dateProvider = new TestDateProvider {Today = new DateTime(2025, 7, 15)};
        _context = new DatabaseContext(options);
        _repository = new ToDoRepository(_context, _dateProvider);   
    }
    
    [Fact]
    public async Task Create_ValidData_ReturnsCorrectToDo()
    {
        var expiry = _dateProvider.Today.AddDays(1);
        var title = "Test";
        var description = "Test desc";

        var todo = await _repository.Create(expiry, title, description);

        Assert.NotNull(todo);
        Assert.Equal(title, todo.Title);
        Assert.Equal(description, todo.Description);
        Assert.Equal(expiry, todo.Expiry);
        Assert.Equal(1, _context.ToDos.Count());
    }

    [Fact]
    public async Task GetById_ExistingId_ReturnsToDo()
    {
        var created = await _repository.Create(_dateProvider.Today, "Title", "Desc");
        var fetched = await _repository.GetById(created.Id);
        
        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched!.Id);
    }

    [Fact]
    public async Task Update_ValidChanges_UpdatesToDoCorrectly()
    {
        var todo = await _repository.Create(_dateProvider.Today, "Old", "Old");

        await _repository.Update(todo, _dateProvider.Today.AddDays(1), "New", "New", 50, false);
        var updated = await _repository.GetById(todo.Id);
        
        Assert.Equal("New", updated!.Title);
        Assert.Equal(50, updated.PercentCompletion);
    }

    [Fact]
    public async Task Delete_ExistingToDo_RemovesToDo()
    {
        var todo = await _repository.Create(_dateProvider.Today, "T", "D");

        await _repository.Delete(todo);
        var found = await _repository.GetById(todo.Id);
        
        Assert.Null(found);
    }

    [Fact]
    public async Task GetAllIncomingToday_TwoDates_ReturnsOnlyTodayItem()
    {
        await _repository.Create(_dateProvider.Today, "Today", "Test");
        await _repository.Create(_dateProvider.Today.AddDays(1), "Tomorrow", "Test");

        var incomingToday = await _repository.GetAllIncomingToday();
        
        Assert.Single(incomingToday!);
    }
    
    [Fact]
    public async Task GetAllIncomingTomorrow_TwoDates_ReturnsOnlyTomorrowItem()
    {
        await _repository.Create(_dateProvider.Today.AddDays(1), "Today", "Test");
        await _repository.Create(_dateProvider.Today.AddDays(2), "Tomorrow", "Test");

        var incomingTomorrow = await _repository.GetAllIncomingTomorrow();
        
        Assert.Single(incomingTomorrow!);
    }
    
    [Fact]
    public async Task GetAllIncomingCurrentWeek_FourDates_ReturnsAllFourItems()
    {
        for (int i = 0; i < 8; i++)
        {
            await _repository.Create(_dateProvider.Today.AddDays(i), $"day {i+1}", $"Test {i+1}");
        }

        var incomingTomorrow = await _repository.GetAllIncomingCurrentWeek();
        
        Assert.Equal(6, incomingTomorrow!.Count);
    }
    
    [Fact]
    public async Task GetAll_ReturnsAllItems()
    {
        await _repository.Create(_dateProvider.Today, "T1", "D1");
        await _repository.Create(_dateProvider.Today, "T2", "D2");

        var all = await _repository.GetAll();
    
        Assert.Equal(2, all!.Count);
    }
}
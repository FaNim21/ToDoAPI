using ToDoApp.Infrastructure.Interfaces;

namespace ToDoApp.Infrastructure.Repositories;

public class SystemDateProvider : IDateProvider
{
    public DateTime Today => DateTime.Today;
}
using ToDoApp.Infrastructure.Interfaces;

namespace ToDoApp.Tests.Helpers;

public class TestDateProvider : IDateProvider
{
    public DateTime Today { get; set; }
}
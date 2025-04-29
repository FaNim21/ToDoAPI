namespace ToDoApp.Infrastructure.Interfaces;

public interface IDateProvider
{
    DateTime Today { get; }
}
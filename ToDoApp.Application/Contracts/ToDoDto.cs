namespace ToDoApp.Application.Contracts;

public record ToDoDto(int Id, DateTime Expiry, string Title, string Description, int PercentCompletion, bool IsCompleted);

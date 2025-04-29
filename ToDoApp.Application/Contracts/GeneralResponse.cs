namespace ToDoApp.Application.Contracts;

public record GeneralResponse(DateTime Expiry, string Title, string Description, int PercentCompletion, bool IsCompleted);
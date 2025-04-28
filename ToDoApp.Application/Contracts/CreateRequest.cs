namespace ToDoApp.Application.Contracts;

public record CreateRequest(DateTime Expiry, string Title, string Description);
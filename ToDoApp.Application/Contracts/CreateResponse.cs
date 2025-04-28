namespace ToDoApp.Application.Contracts;

public record CreateResponse(int Id, DateTime Expiry, string Title, string Description);
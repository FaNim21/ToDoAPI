namespace ToDoApp.Domain.Entities;

public class ToDo
{
    public int Id { get; set; }
    public DateTime Expiry { get; set; } = DateTime.Now;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PercentCompletion { get; set; }
    public bool IsCompleted { get; set; }
}
namespace ToDo.Domain.Entities;

public class ToDo
{
    public DateTime Expiry { get; private set; } = DateTime.Now;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool IsCompleted { get; private set; }
}
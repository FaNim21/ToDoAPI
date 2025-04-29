using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Domain.Entities;

public class ToDo
{
    public int Id { get; set; }
    [Required] public DateTime Expiry { get; set; } = DateTime.Now;
    [Required, StringLength(100)] public string Title { get; set; } = string.Empty;
    [Required, StringLength(240)] public string Description { get; set; } = string.Empty;
    public int PercentCompletion { get; set; }
    public bool IsCompleted { get; set; }
}
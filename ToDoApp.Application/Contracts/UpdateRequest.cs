using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Application.Contracts;

public record UpdateRequest(int Id, [Required] DateTime Expiry, [Required, StringLength(100)] string Title, [Required, StringLength(240)] string Description, [Range(0, 100)]int PercentCompletion, bool IsCompleted);
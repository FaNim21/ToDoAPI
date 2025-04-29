using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Application.Contracts;

public record CreateRequest([Required] DateTime Expiry, [Required, StringLength(100)] string Title, [Required, StringLength(240)] string Description);
using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Infrastructure.Data;

public class DatabaseContext : DbContext
{
    public DbSet<ToDo> ToDos { get; set; }

    
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}
}
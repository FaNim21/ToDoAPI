using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ToDoApp.Infrastructure.Data;

namespace ToDoApp.Infrastructure.Factories;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

        string connection = "server=localhost;port=3306;database=todo_db;user=root;password=mysql1234";
        optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));

        return new DatabaseContext(optionsBuilder.Options);
    }
}
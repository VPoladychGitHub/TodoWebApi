using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TodoWebApi.Models;
namespace TodoWebApi.DataAccess
{
    public class TodoContext: DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options):base(options)
        {
           // Database.EnsureCreated();
        }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}

using Domain_Layer.Models;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure_Layer.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entities if needed here
            base.OnModelCreating(modelBuilder);
        }
    }
}

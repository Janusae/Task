using Microsoft.EntityFrameworkCore;
using Task = TaskService.Domain.DB.Sql.Task;

namespace TaskService.Infrastructure.DbContexts
{
    public class ProgramDbContext : DbContext
    {
        public ProgramDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TaskService.Domain.DB.Sql.Task> Tasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Data
            modelBuilder.Entity<Task>().HasData(new Task
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Description = "This is Test",
                Title = "Task Number one",
                DueDate = new DateTime(2025, 9, 9),
                UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                CreatedAt = new DateTime(2025, 9, 9)
            });

        }
    }
}

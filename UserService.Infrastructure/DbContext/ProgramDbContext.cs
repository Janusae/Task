using Microsoft.EntityFrameworkCore;
using UserService.Domain.DB.Sql;

namespace UserService.Infrastructure.DbContexts
{
    public class ProgramDbContext : DbContext
    {
        public ProgramDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProfile>()
                .HasKey(up => up.UserId);

            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<UserProfile>(up => up.UserId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Seed Data
            var userId = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                UserName = "Admin",
                Email = "admin@example.com",
                PasswordHash = "hashedpassword",
                CreatedAt = new DateTime(2025, 9, 9)
            });

            modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            {
                UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                FirstName = "System",
                LastName = "Admin",
                AvatarUrl = null,
                Bio = "Default admin user"
            });
        }
    }
}

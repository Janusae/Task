namespace UserService.Domain.DB.Sql
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public DateTime CreatedAt { get; set; }

        public UserProfile? Profile { get; set; }
        public UserRole UserRole { get; set; }

    }
    public enum UserRole
    {
        User , 
        Admin

    }
}

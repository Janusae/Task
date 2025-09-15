namespace UserService.Domain.DB.Sql
{
    public class UserProfile
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }

        public User User { get; set; } = default!;
    }
}

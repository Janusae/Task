namespace UserService.Application.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public UserProfileDto? Profile { get; set; }
    }

    public class UserProfileDto
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}

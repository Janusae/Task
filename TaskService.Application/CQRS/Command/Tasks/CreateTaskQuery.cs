//using MediatR;

//namespace TaskService.Application.CQRS.Command.Tasks
//{
//    public class CreateUserCommand : IRequest<Guid>
//    {
//        public CreateUserDto CreateUserDto { get; set; } = default!;
//    }

//    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
//    {
//        private readonly ProgramDbContext _dbContext;

//        public CreateUserCommandHandler(ProgramDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task<Guid> Handle(CreateUserCommand requests, CancellationToken cancellationToken)
//        {
//            var userId = Guid.NewGuid();
//            var request = requests.CreateUserDto;
//            var user = new User
//            {
//                Id = userId,
//                UserName = request.UserName,
//                Email = request.Email,
//                PasswordHash = request.Password, 
//                CreatedAt = DateTime.UtcNow,
//                UserRole = UserRole.User,
//                Profile = new UserProfile
//                {
//                    UserId = userId,
//                    FirstName = request.FirstName,
//                    LastName = request.LastName
//                }
//            };

//            _dbContext.Users.Add(user);
//            await _dbContext.SaveChangesAsync(cancellationToken);

//            return userId;
//        }
//    }
//}
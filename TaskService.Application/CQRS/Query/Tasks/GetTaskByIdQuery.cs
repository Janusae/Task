//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using UserService.Application.Dto;
//using UserService.Domain.DB.Sql;
//using UserService.Infrastructure.DbContexts;

//namespace TaskService.Application.CQRS.Command.Tasks
//{
//    public class GetUserByIdQuery : IRequest<UserDto>
//    {
//        public Guid Id { get; set; }
//    }
//    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
//    {
//        private readonly ProgramDbContext _dbContext;

//        public GetUserByIdQueryHandler(ProgramDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
//        {
//            var user = await _dbContext.Users
//                .Include(u => u.Profile)
//                .AsNoTracking()
//                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

//            if (user == null) return null;

//            return new UserDto
//            {
//                Id = user.Id,
//                UserName = user.UserName,
//                Profile = user.Profile == null ? null : new UserProfileDto
//                {
//                    UserId = user.Profile.UserId,
//                    FirstName = user.Profile.FirstName,
//                    LastName = user.Profile.LastName
//                }
//            };
//        }
//    }
//}

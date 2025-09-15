using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Dto;
using UserService.Domain.DB.Sql;
using UserService.Infrastructure.DbContexts;

namespace UserService.Application.CQRS.Command.Users
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>
    {
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly ProgramDbContext _dbContext;

        public GetAllUsersQueryHandler(ProgramDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                 .Select(u => new UserDto
                 {
                     Id = u.Id,
                     UserName = u.UserName,
                     Profile = new UserProfileDto
                     {
                         UserId = u.Profile.UserId,
                         FirstName = u.Profile.FirstName,
                         LastName = u.Profile.LastName
                     }
                 })
                 .ToListAsync(cancellationToken);
        }
    }
}

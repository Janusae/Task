using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Dto;
using UserService.Infrastructure.DbContexts;

namespace UserService.Application.CQRS.Command.Users
{
    public class UpdateUserCommand : IRequest<string>
    {
        public UpdateUserDto UpdateUserDto { get; set; } = default!;
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string>
    {
        private readonly ProgramDbContext _dbContext;

        public UpdateUserCommandHandler(ProgramDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FindAsync(new object[] { request.UpdateUserDto.Id }, cancellationToken);
            var profile = await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (user is null) throw new Exception("User not found");

            if (!string.IsNullOrEmpty(request.UpdateUserDto.UserName)) user.UserName = request.UpdateUserDto.UserName;
            if (!string.IsNullOrEmpty(request.UpdateUserDto.Email)) user.Email = request.UpdateUserDto.Email;
            if (!string.IsNullOrEmpty(request.UpdateUserDto.Password)) user.PasswordHash = request.UpdateUserDto.Password;

            if (user.Profile != null)
            {
                if (!string.IsNullOrEmpty(request.UpdateUserDto.FirstName)) profile.FirstName = request.UpdateUserDto.FirstName;
                if (!string.IsNullOrEmpty(request.UpdateUserDto.LastName)) profile.LastName = request.UpdateUserDto.LastName;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return "User updated successfully";
        }
    }
}

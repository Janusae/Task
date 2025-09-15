using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Infrastructure.DbContexts;

namespace UserService.Application.CQRS.Command.Users
{
    public class DeleteUserCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
    {
        private readonly ProgramDbContext _dbContext;

        public DeleteUserCommandHandler(ProgramDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(new object[] { request.Id }, cancellationToken);
            if (user is null) throw new Exception("User not found");

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return "User deleted successfully";
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskService.Infrastructure.DbContexts;

namespace TaskService.Application.CQRS.Command.Tasks
{
    public class DeleteTaskQuery : IRequest<string>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteTaskQuery, string>
    {
        private readonly ProgramDbContext _dbContext;

        public DeleteUserCommandHandler(ProgramDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(DeleteTaskQuery request, CancellationToken cancellationToken)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (task is null) throw new Exception("task not found");

            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return "task deleted successfully";
        }
    }
}

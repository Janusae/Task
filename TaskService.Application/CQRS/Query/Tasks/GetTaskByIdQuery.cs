using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskService.Application.Dto;
using TaskService.Infrastructure.DbContexts;

namespace TaskService.Application.CQRS.Command.Tasks
{
    public class GetTaskByIdQuery : IRequest<TaskDto>
    {
        public Guid Id { get; set; }
    }
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto?>
    {
        private readonly ProgramDbContext _dbContext;

        public GetTaskByIdQueryHandler(ProgramDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TaskDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null) return null;

            return new TaskDto
            {
                Id = user.Id,
                Title = user.Title,
                Description = user.Description,
                CreatedAt = user.CreatedAt,
                DueDate = user.DueDate,
                UserId = user.UserId
            };
        }
    }
}

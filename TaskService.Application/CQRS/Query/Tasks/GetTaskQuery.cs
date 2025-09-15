using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskService.Application.Dto;
using TaskService.Infrastructure.DbContexts;

namespace TaskService.Application.CQRS.Command.Tasks
{
    public class GetTaskQuery : IRequest<List<TaskDto>>
    {
    }

    public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, List<TaskDto>>
    {
        private readonly ProgramDbContext _dbContext;

        public GetTaskQueryHandler(ProgramDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TaskDto>> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Tasks
                 .Select(u => new TaskDto
                 {
                     Id = u.Id,
                     Title = u.Title,
                     Description = u.Description,
                     CreatedAt = u.CreatedAt,
                     DueDate = u.DueDate,
                     UserId = u.UserId
                 })
                 .ToListAsync(cancellationToken);
        }
    }
}

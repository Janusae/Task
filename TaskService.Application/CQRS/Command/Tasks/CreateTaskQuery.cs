using MediatR;
using TaskService.Application.Dto;
using TaskService.Infrastructure.DbContexts;

namespace TaskService.Application.CQRS.Command.Tasks
{
    public class CreateTaskQuery : IRequest<Guid>
    {
        public CreateTaskDto CreateUserDto { get; set; } = default!;
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateTaskQuery, Guid>
    {
        private readonly ProgramDbContext _dbContext;
        private readonly HttpClient _httpClient;

        public CreateUserCommandHandler(ProgramDbContext dbContext, HttpClient httpClient)
        {
            _dbContext = dbContext;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7142");
        }

        public async Task<Guid> Handle(CreateTaskQuery requests, CancellationToken cancellationToken)
        {
            var userId = Guid.NewGuid();
            var request = requests.CreateUserDto;
            var response = await _httpClient.GetAsync($"/api/Users/{userId}");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Response is incorrect!");

            var content = await response.Content.ReadAsStringAsync();

            var Task = new Domain.DB.Sql.Task
            {
                Id = userId,
                Title = request.Title,
                Description = request.Description,
                CreatedAt = DateTime.Now,
                DueDate = request.DueDate,
                UserId = request.UserId
            };

            _dbContext.Tasks.Add(Task);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return userId;
        }
    }
}
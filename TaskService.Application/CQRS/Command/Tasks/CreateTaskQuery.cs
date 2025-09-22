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
            var request = requests.CreateUserDto;

            // از UserId موجود در ورودی استفاده کن
            var response = await _httpClient.GetAsync($"/api/Users");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Response is incorrect!");

            var content = await response.Content.ReadAsStringAsync();

            // لیست رو دسیریالایز کن
            var users = System.Text.Json.JsonSerializer.Deserialize<List<UserDto>>(content,
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // چک کن کاربر وجود داره یا نه
            var user = users?.FirstOrDefault(u => u.Id == request.UserId);
            if (user is null)
                throw new Exception("User not found in UserService");

            // حالا تسک رو بساز
            var task = new Domain.DB.Sql.Task
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                CreatedAt = DateTime.Now,
                DueDate = request.DueDate,
                UserId = request.UserId
            };

            _dbContext.Tasks.Add(task);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return task.Id;
        }
    }
}
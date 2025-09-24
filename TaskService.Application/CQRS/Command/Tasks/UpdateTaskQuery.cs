using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskService.Application.Dto;
using TaskService.Infrastructure.DbContexts;

namespace TaskService.Application.CQRS.Command.Tasks
{
    public class UpdateTaskQuery : IRequest<string>
    {
        public UpdateTaskDto UpdateTaskDto { get; set; } = default!;
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateTaskQuery, string>
    {
        private readonly ProgramDbContext _dbContext;
        private readonly HttpClient _httpClient;

        public UpdateUserCommandHandler(ProgramDbContext dbContext, HttpClient httpClient)
        {
            _dbContext = dbContext;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7142");
        }

        public async Task<string> Handle(UpdateTaskQuery request, CancellationToken cancellationToken)
        {
            var updateTaskDto = request.UpdateTaskDto;
            var task = await _dbContext.Tasks
                .FirstOrDefaultAsync(p => p.Id == updateTaskDto.Id, cancellationToken);
            if (task is null) throw new Exception("Task not found");

            var response = await _httpClient.GetAsync($"/api/Users/{request.UpdateTaskDto.UserId}");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Response is Incorrect!");

            var content = await response.Content.ReadAsStringAsync();
            if (content == "") return "User not found!";

            var user = System.Text.Json.JsonSerializer.Deserialize<UserDto>
                (content, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (!string.IsNullOrEmpty(updateTaskDto.Title)) task.Title = updateTaskDto.Title;
            if (!string.IsNullOrEmpty(updateTaskDto.Title)) task.Description = updateTaskDto.Description;
            if (!string.IsNullOrEmpty(updateTaskDto.Title)) task.DueDate = updateTaskDto.DueDate;
            if (!string.IsNullOrEmpty(updateTaskDto.Title)) task.UserId = updateTaskDto.Id;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return "Task updated successfully";
        }
    }
}

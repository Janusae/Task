using Microsoft.EntityFrameworkCore;
using TaskService.Application.CQRS.Command.Tasks;
using TaskService.Infrastructure.DbContexts;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ProgramDbContext>(options =>
{
    options.UseSqlServer("Server = . ; Database = TaskManagement  ; TrustServerCertificate = True ; Trusted_Connection = True ; ");
});
builder.Services.AddHttpClient("UserService", client =>
{
    client.BaseAddress = new Uri("https://localhost:7142");
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetTaskQuery>());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

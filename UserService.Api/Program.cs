using Microsoft.EntityFrameworkCore;
using UserService.Application.CQRS.Command.Users;
using UserService.Infrastructure.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ProgramDbContext>(options =>
{
    options.UseSqlServer("Server = . ; Database = UserManagement  ; TrustServerCertificate = True ; Trusted_Connection = True ");
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllUsersQuery>());
builder.Services.AddSwaggerGen();
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

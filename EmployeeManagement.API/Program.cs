using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Application.Validators;
using EmployeeManagement.Infrastructure;
using EmployeeManagement.Infrastructure.Persistence;
using EmployeeManagement.API.Middleware;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddTransient<GlobalExceptionMiddleware>();
builder.Services.AddEndpointsApiExplorer(); builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<CreateEmployeeRequestValidator>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddInfrastructure(builder.Configuration);
var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.UseHttpsRedirection(); app.MapControllers();
using (var scope = app.Services.CreateScope()) { await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync(); }
app.Run();

public partial class Program;

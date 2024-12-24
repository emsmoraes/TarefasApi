using Microsoft.EntityFrameworkCore;
using TarefasApi.Data;
using TarefasApi.Repositories;
using TarefasApi.Services;

var builder = WebApplication.CreateBuilder(args);

string? stringConnection = builder.Configuration.GetConnectionString("DefaultConnection");

if (stringConnection is null)
{
    throw new Exception("Connection string not found");
}

builder.Services.AddDbContext<TasksContext>(options =>
    options.UseNpgsql(stringConnection));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();

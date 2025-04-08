using dotenv.net;
using Microsoft.EntityFrameworkCore;
using TaskManagement_RESTAPI.AppDataContext;
using TaskManagement_RESTAPI.Exceptions;
using TaskManagement_RESTAPI.Repositories.Concrete;
using TaskManagement_RESTAPI.Repositories.Interfaces;
using TaskManagement_RESTAPI.Services.Concrete;
using TaskManagement_RESTAPI.Services.Contracts;


var builder = WebApplication.CreateBuilder(args);


DotEnv.Load();

Console.WriteLine(Environment.GetEnvironmentVariable("CONNECTIONSTRING"));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        Environment.GetEnvironmentVariable("CONNECTIONSTRING"),
        new MySqlServerVersion(new Version(8, 0, 34))
    )
);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddScoped<ITaskItemService, TaskItemService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
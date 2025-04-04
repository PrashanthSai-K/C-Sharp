using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using task10.AppDataContext;
using task10.Interface;
using task10.Services;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var conn = Environment.GetEnvironmentVariable("CONNECTIONSTRING");

Console.WriteLine(conn, "coon str");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(conn,
    new MySqlServerVersion(new Version(8, 0, 34)))
);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IBookRepository, BookService>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5106);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
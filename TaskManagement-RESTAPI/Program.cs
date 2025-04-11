using System.Security.Cryptography.Xml;
using System.Threading.RateLimiting;
using dotenv.net;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TaskManagement_RESTAPI.AppDataContext;
using TaskManagement_RESTAPI.Exceptions;
using TaskManagement_RESTAPI.Repositories.Concrete;
using TaskManagement_RESTAPI.Repositories.Interfaces;
using TaskManagement_RESTAPI.Security;
using TaskManagement_RESTAPI.Services.Concrete;
using TaskManagement_RESTAPI.Services.Contracts;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT AUthentication",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = "Bearer"}
            },
            []
        }
    });
});

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

builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new JwtTokenGenerator().TokenValidationParameters();
        });

builder.Services.AddRateLimiter(rateLimitingOptions =>
{
    rateLimitingOptions.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetSlidingWindowLimiter(
            partitionKey: (httpContext.Connection.RemoteIpAddress?.ToString() ?? httpContext.User.Identity?.Name) ?? "default",
            factory: partition => new SlidingWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                SegmentsPerWindow = 6,
                PermitLimit = 20,
                QueueLimit = 10,
                Window = TimeSpan.FromSeconds(60)
            }
        )
    );
    rateLimitingOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

builder.Services.AddAuthorization();

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

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
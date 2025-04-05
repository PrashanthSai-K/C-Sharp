using System.Text;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using task10.AppDataContext;
using task10.Interface;
using task10.Middleware;
using task10.Services;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var conn = Environment.GetEnvironmentVariable("CONNECTIONSTRING");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(conn,
    new MySqlServerVersion(new Version(8, 0, 34)))
);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authrorization",
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

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IBookRepository, BookService>();
builder.Services.AddScoped<IUserRepository, UserService>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5106);
});

var jwtKey = Environment.GetEnvironmentVariable("JWTKEY") ?? throw new Exception("JWTKEY environment variable is not set");

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Environment.GetEnvironmentVariable("API"),
            ValidAudience = Environment.GetEnvironmentVariable("API"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    }
);

builder.Services.AddAuthorization();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLogMiddleware>();
app.UseMiddleware<ResponseLogMiddleware>();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
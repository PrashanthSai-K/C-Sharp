# Task (Additional): Task Management REST API

### 🎯Objective :

- To build a Task Management REST API with ASP.NET Core and Entity Framework Core by following best practices in architecture like,

    - **Repository Pattern** with a centralized manager

    - **Service Layer Abstraction** with contracts and service manager

    - **Authentication and Authorization** using JWT and Refresh Tokens

    - **CRUD Operations** for users and tasks

    - **Advanced Querying** with search, filter, sorting, and pagination


###  🔧 Architecture Overview

#### Repository Pattern with Manager

- Generic Repository: Implements common data access logic.

- Concrete Repositories: Implement entity-specific logic (e.g., UserRepository, TaskRepository).

- Repository Manager: Centralizes access to all repositories and manages the ApplicationDbContext.

```
Repositories/
  - Base/
    - GenericRepository.cs
  - Concrete/
    - RepositotyManager.cs  / RepositoryManager.cs
    - TaskItemRepository.cs  / TaskRepository.cs
    - UserRepository.cs  / UserRepository.cs
  - Interfaces
    - IGenericRepository.cs
    - IUserRepository.cs 
    - ITaskRepository.cs 
    - IRepositoryManager.cs 
```
#### Service Layer

- Service Contracts: Interfaces for services.

- Service Implementations: Contain business logic.

- Service Manager: Aggregates all services.

```
Services/
  - Contracts/
    - IUserService.cs
    - ITaskService.cs
    - IServiceManager.cs
  - Concrete/
    - UserService.cs
    - TaskService.cs
    - ServiceManager.cs / ServiceManager.cs
```

### 🔐 Authentication & Refresh Tokens
 
- JWT Access Tokens are issued upon login.

- Refresh Tokens are generated and stored in the database.

- Endpoint provided to renew tokens.

- Endpoints have been authorized with `[Authorize]` attribute

#### Refresh Token Flow

- `/api/login` returns access and refresh token.

- Client stores both.

- If access token expires, send refresh token to `/api/refresh`.

- Server validates and returns a new access + refresh token.

### ⚠️ Global Exception Handling

A centralized middleware catches exceptions and returns standardized error responses.

```
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

app.UseExceptionHandler();

```

```
public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
{
    var errorResponse = new ErrorResponse();

    _logger.LogInformation($"{exception.Message}");
    errorResponse.Message = exception.Message;

    switch (exception)
    {
        case UserNotFoundException:
            errorResponse.StatusCode = 404;
            httpContext.Response.StatusCode = 404;
            break;
        case TaskNotFoundException:
            errorResponse.StatusCode = 404;
            httpContext.Response.StatusCode = 404;
            break;
        case UsernameNotFoundException:
            errorResponse.StatusCode = 401;
            httpContext.Response.StatusCode = 401;
            break;
        case LoginPasswordWrongException:
            errorResponse.StatusCode = 401;
            httpContext.Response.StatusCode = 401;
            break;
        default:
            errorResponse.StatusCode = 500;
            httpContext.Response.StatusCode = 500;
            break;
    }

    await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
    return true;
}
```


### 📬 API Endpoints
- All endpoints are protected except login.
- Used JWT token authentication.

#### AuthController

- `POST /api/login` - Authenticate and return tokens

- `POST /api/refresh` - Refresh access token

#### UserController

- `GET /api/users/` -Get all users

- `GET /api/users/{id}` -Get a specific user

- `POST /api/users` - Create user

- `PUT /api/users` - Update user details

- `DELETE /api/users` - Detele a user


#### TaskController

- `GET /api/tasks` - Get all tasks (supports filters)

- `POST /api/tasks` - Create new task

- `PUT /api/tasks/{id}` - Update task

- `DELETE /api/tasks/{id}` - Delete task


### 🔍 Filtering, Sorting, and Pagination

- Supported via TaskQueryParams:

```
public class TaskQueryParams
{
    public int? UserId {get;set;}
    public bool? IsCompleted {get;set;}
    public bool? OverDue {get;set;}
    public string? SortBy {get;set;} = "DueDate";
    public bool Descending {get;set;} = false;

    public DateTime StartDate {get;set;}
    public DateTime EndDate {get;set;}

    public string? SearchTerm {get;set;}
    
    const int MaxPageSize = 50;
    public int PageNumber {get;set;} = 1;

    private int _pageSize = 10;
    public int PageSize {
        get 
        {
            return _pageSize;
        }set
        {
            _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
```

- Applied this params in service layer of the application.

#### Sample query

```
http://localhost:5085/api/Tasks?UserId=1&IsCompleted=false&OverDue=false&SortBy=duedate&Descending=true&StartDate=2025-04-08&EndDate=2025-04-09&SearchTerm=string&PageNumber=3&PageSize=1
```
### 🚦 Rate Limiting

- **Rate Limiting** : Restricting the number of requests a client can make to an API in a given time window. Uses Client IP to impose limits.

- **Sliding Window** : A type of rate-limiting mechanism that maintains the rate limit dynamically based on the time of the requests.

```
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


app.UseRateLimiter();
```

- After configurnig our global rate limiter all our api enpoints will be rate limited.
- If we want to disable rate limiter for a particular  controller class or API method, we can add `[DisableRateLimiting]` attribute. 


### 🧪 Packages Used :

- ASP.NET Core Web API

- Entity Framework Core

- MySQL

- AutoMapper

- JWT (System.IdentityModel.Tokens.Jwt)

- Swagger (Swashbuckle)

### ✅ Getting Started

- Clone the repo

- Configure DB connection by creating `.env` file in project root foler

- Update Database with entities defined:

```
dotnet ef database update
```

- Run the API:

```
dotnet run
```

### 📌 Output

![image](https://github.com/user-attachments/assets/760e1d11-3fa2-40bd-a81f-f167947ef420)

![image](https://github.com/user-attachments/assets/a4d25d1b-85a5-4f34-98c8-cf7b9bfb341f)

![image](https://github.com/user-attachments/assets/172c1299-e25b-473e-b9fd-a0898e460f35)

![image](https://github.com/user-attachments/assets/125cb256-cbe0-438a-95ab-7d0eb42de58c)

![image](https://github.com/user-attachments/assets/796140e4-4d0e-4d9f-9318-9d863500bf16)



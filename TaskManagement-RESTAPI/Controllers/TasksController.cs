using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using TaskManagement_RESTAPI.Entities.Models;
using TaskManagement_RESTAPI.Entities.RequestParams;
using TaskManagement_RESTAPI.Services.Contracts;
using TaskManagement_RESTAPI.Shared.DTO;

namespace TaskManagement_RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ICacheService _cache;

        public TasksController(IServiceManager serviceManager, ICacheService cacheService)
        {
            _serviceManager = serviceManager;
            _cache = cacheService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        [DisableRateLimiting]
        public async Task<IActionResult> GetAllTasksAsync([FromQuery] TaskQueryParams queryParams)
        {
            var key = $"GetAllTasks:{queryParams.UserId}:{queryParams.IsCompleted}:{queryParams.OverDue}:{queryParams.SortBy}:{queryParams.Descending}:{queryParams.StartDate}:{queryParams.EndDate}:{queryParams.SearchTerm}:{queryParams.PageNumber}:{queryParams.PageSize}";
            var data = await _cache.GetCacheAsync<IEnumerable<TaskItem>>(key);
            if (data != null)
            {
                return Ok(new { Message = "From cache", data = data });
            }
            var tasks = await _serviceManager.TaskItemService.GetAllTasks(queryParams);
            await _cache.SetCacheAsync<IEnumerable<TaskItem>>(key, tasks, TimeSpan.FromSeconds(30));
            await _cache.AddKeyToSetAsync("Tasks", key);
            return Ok(new { data = tasks });
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var key = $"GetTaskById:{id}";
            var data = await _cache.GetCacheAsync<TaskItem>(key);
            if (data != null)
            {
                return Ok(data);
            }
            var task = await _serviceManager.TaskItemService.GetTaskById(id);
            await _cache.SetCacheAsync(key, task, TimeSpan.FromMinutes(30));
            await _cache.AddKeyToSetAsync("Tasks", key);
            return Ok(task);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> CreateTask(CreateTask task)
        {
            await _serviceManager.TaskItemService.CreateTask(task);
            await _cache.InvalidateAllKeysInSet("Tasks");
            return Ok(new { Message = "Task created successfully" });
        }

        [HttpPut]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateTask(UpdateTask task)
        {
            await _serviceManager.TaskItemService.UpdateTask(task);
            await _cache.InvalidateAllKeysInSet("Tasks");
            return Ok(new { Message = "Task updated successfully" });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _serviceManager.TaskItemService.DeleteTask(id);
            await _cache.InvalidateAllKeysInSet("Tasks");
            return Ok(new { Message = "Task deleted successfully" });
        }
    }
}

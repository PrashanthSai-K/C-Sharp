using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public TasksController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAllTasksAsync([FromQuery] TaskQueryParams queryParams)
        {
            Console.WriteLine("called");
            var tasks = await _serviceManager.TaskItemService.GetAllTasks(queryParams);
            return Ok(new { data = tasks });
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _serviceManager.TaskItemService.GetTaskById(id);
            return Ok(task);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> CreateTask(CreateTask task)
        {
            await _serviceManager.TaskItemService.CreateTask(task);
            return Ok(new { Message = "Task created successfully" });
        }

        [HttpPut]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateTask(UpdateTask task)
        {
            await _serviceManager.TaskItemService.UpdateTask(task);
            return Ok(new { Message = "Task updated successfully" });
        }

        [HttpDelete]    
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _serviceManager.TaskItemService.DeleteTask(id);
            return Ok(new { Message = "Task deleted successfully" });
        }
    }
}

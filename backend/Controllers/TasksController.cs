using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = new[]
            {
                new { id = 1, name = "Task from backend 1", description = "Backend task details 1" },
                new { id = 2, name = "Task from backend 2", description = "Backend task details 2" },
                new { id = 3, name = "Task from backend 3", description = "Backend task details 3" }
            };

            return Ok(tasks);
        }
    }
}

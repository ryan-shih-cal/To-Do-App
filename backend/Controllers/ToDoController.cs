using Microsoft.AspNetCore.Mvc;
using ToDoApp.Factory;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoServiceFactory _serviceFactory;

        public ToDoController(IToDoServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDoItem>>> GetAll([FromQuery] string provider = "sqlite")
        {
            var service = _serviceFactory.GetService(provider);
            var items = await service.GetAllAsync();
            return Ok(items);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetById(int id, string provider)
        {
            var service = _serviceFactory.GetService(provider);
            var item = await service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ToDoItem item, string provider)
        {
            var service = _serviceFactory.GetService(provider);
            await service.AddAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ToDoItem item, string provider)
        {
            if (id != item.Id) return BadRequest("Mismatched ID");

            var service = _serviceFactory.GetService(provider);
            await service.UpdateAsync(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, string provider)
        {
            var service = _serviceFactory.GetService(provider);
            await service.DeleteAsync(id);
            return NoContent();
        }
    }
}

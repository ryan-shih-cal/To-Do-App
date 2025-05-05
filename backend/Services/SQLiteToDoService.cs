using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;
using ToDoApp.Services.Interfaces;
using ToDoApp.Data;

namespace ToDoApp.Services.Sqlite
{
    public class SqliteToDoService : IToDoService
    {
        private readonly AppDbContext _context;

        public SqliteToDoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ToDoItem>> GetAllAsync()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        public async Task<ToDoItem?> GetByIdAsync(int id)
        {
            return await _context.ToDoItems.FindAsync(id);
        }

        public async Task AddAsync(ToDoItem item)
        {
            Console.WriteLine($"[SQLITE] Adding item: {item.Name}");
            _context.ToDoItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ToDoItem item)
        {
            Console.WriteLine($"[SQLITE] Saving item: {item.Name}");
            var existing = await _context.ToDoItems.FindAsync(item.Id);
            if (existing != null)
            {
                existing.Name = item.Name;
                existing.Description = item.Description;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            Console.WriteLine($"[SQLITE] Deleting item: {id}");
            var item = await _context.ToDoItems.FindAsync(id);
            if (item != null)
            {
                _context.ToDoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}

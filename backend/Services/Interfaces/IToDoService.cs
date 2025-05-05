using ToDoApp.Models;

namespace ToDoApp.Services.Interfaces
{
    public interface IToDoService
    {
        Task<List<ToDoItem>> GetAllAsync();
        Task<ToDoItem?> GetByIdAsync(int id);
        Task AddAsync(ToDoItem item);
        Task UpdateAsync(ToDoItem item);
        Task DeleteAsync(int id);
    }
}

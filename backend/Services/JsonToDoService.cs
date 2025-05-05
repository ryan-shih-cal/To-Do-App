using System.Text.Json;
using ToDoApp.Models;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Json
{
    public class JsonToDoService : IToDoService
    {
        private const string FilePath = "todoitems.json";

        public async Task<List<ToDoItem>> GetAllAsync()
        {
            if (!File.Exists(FilePath))
                return new List<ToDoItem>();

            var json = await File.ReadAllTextAsync(FilePath);
            var items = JsonSerializer.Deserialize<List<ToDoItem>>(json);
            return items ?? new List<ToDoItem>();
        }

        public async Task<ToDoItem?> GetByIdAsync(int id)
        {
            var items = await GetAllAsync();
            return items.Find(i => i.Id == id);
        }

        public async Task AddAsync(ToDoItem item)
        {
            var items = await GetAllAsync();
            item.Id = items.Count > 0 ? items.Max(i => i.Id) + 1 : 1;
            items.Add(item);
            await SaveAllAsync(items);
        }

        public async Task UpdateAsync(ToDoItem item)
        {
            var items = await GetAllAsync();
            var index = items.FindIndex(i => i.Id == item.Id);
            if (index != -1)
            {
                items[index] = item;
                await SaveAllAsync(items);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var items = await GetAllAsync();
            items.RemoveAll(i => i.Id == id);
            await SaveAllAsync(items);
        }

        private async Task SaveAllAsync(List<ToDoItem> items)
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(FilePath, json);
        }
    }
}

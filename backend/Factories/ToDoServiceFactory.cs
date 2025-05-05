using ToDoApp.Services.Interfaces;
using ToDoApp.Services.Json;
using ToDoApp.Services.Sqlite;

namespace ToDoApp.Factory
{
    public class ToDoServiceFactory : IToDoServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ToDoServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IToDoService GetService(string provider)
        {
            return provider.ToLower() switch
            {
                "sqlite" => _serviceProvider.GetRequiredService<SqliteToDoService>(),
                "json" => _serviceProvider.GetRequiredService<JsonToDoService>(),
                _ => throw new ArgumentException($"Unsupported provider: {provider}")
            };
        }
    }
}

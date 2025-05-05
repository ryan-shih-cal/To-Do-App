using ToDoApp.Services.Interfaces;

namespace ToDoApp.Factory
{
    public interface IToDoServiceFactory
    {
        IToDoService GetService(string provider);
    }
}

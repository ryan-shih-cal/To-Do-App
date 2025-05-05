using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Factory;
using ToDoApp.Models;
using ToDoApp.Services.Json;
using ToDoApp.Services.Sqlite;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000");

builder.Services.AddControllers();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Register DbContext, services, etc.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<SqliteToDoService>();
builder.Services.AddScoped<JsonToDoService>();
builder.Services.AddScoped<IToDoServiceFactory, ToDoServiceFactory>();

var app = builder.Build();
Console.WriteLine($"DB Path: {Path.GetFullPath("todo.db")}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // <----- This applies any pending migrations
    
    if (!db.ToDoItems.Any())
{
    db.ToDoItems.Add(new ToDoItem { Name = "Test from Program.cs", Description = "Startup test" });
    db.SaveChanges();
}
}

app.UseHttpsRedirection();

// Enable CORS middleware
app.UseCors("AllowFrontend");

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
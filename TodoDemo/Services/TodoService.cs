using TodoDemo.Models;

namespace TodoDemo.Services;

public class TodoService
{
    private static readonly object _lock = new();
    private static readonly List<TodoItem> _todoItems = new()
    {
        new TodoItem { Id = 1, Title = "Welcome to TODO Demo", Description = "This is a sample TODO item", IsCompleted = false, CreatedDate = DateTime.Now.AddDays(-1) },
        new TodoItem { Id = 2, Title = "Complete the tutorial", Description = "Follow the steps to understand the app", IsCompleted = false, CreatedDate = DateTime.Now.AddDays(-1) },
        new TodoItem { Id = 3, Title = "Add your first TODO", Description = "Try creating your own TODO item", IsCompleted = false, CreatedDate = DateTime.Now }
    };
    
    private static int _nextId = 4;
    
    public List<TodoItem> GetAll()
    {
        lock (_lock)
        {
            return _todoItems.OrderByDescending(t => t.CreatedDate).ToList();
        }
    }
    
    public TodoItem? GetById(int id)
    {
        lock (_lock)
        {
            return _todoItems.FirstOrDefault(t => t.Id == id);
        }
    }
    
    public void Create(TodoItem item)
    {
        lock (_lock)
        {
            item.Id = _nextId++;
            item.CreatedDate = DateTime.Now;
            _todoItems.Add(item);
        }
    }
    
    public bool Update(TodoItem item)
    {
        lock (_lock)
        {
            var existingItem = _todoItems.FirstOrDefault(t => t.Id == item.Id);
            if (existingItem == null) return false;
            
            existingItem.Title = item.Title;
            existingItem.Description = item.Description;
            existingItem.IsCompleted = item.IsCompleted;
            
            return true;
        }
    }
    
    public bool Delete(int id)
    {
        lock (_lock)
        {
            var item = _todoItems.FirstOrDefault(t => t.Id == id);
            if (item == null) return false;
            
            _todoItems.Remove(item);
            return true;
        }
    }
    
    public bool ToggleComplete(int id)
    {
        lock (_lock)
        {
            var item = _todoItems.FirstOrDefault(t => t.Id == id);
            if (item == null) return false;
            
            item.IsCompleted = !item.IsCompleted;
            return true;
        }
    }
}

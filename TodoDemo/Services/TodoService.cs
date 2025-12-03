using TodoDemo.Models;

namespace TodoDemo.Services;

public class TodoService
{
    private static List<TodoItem> _todoItems = new()
    {
        new TodoItem { Id = 1, Title = "Welcome to TODO Demo", Description = "This is a sample TODO item", IsCompleted = false, CreatedDate = DateTime.Now.AddDays(-1) },
        new TodoItem { Id = 2, Title = "Complete the tutorial", Description = "Follow the steps to understand the app", IsCompleted = false, CreatedDate = DateTime.Now.AddDays(-1) },
        new TodoItem { Id = 3, Title = "Add your first TODO", Description = "Try creating your own TODO item", IsCompleted = false, CreatedDate = DateTime.Now }
    };
    
    private static int _nextId = 4;
    
    public List<TodoItem> GetAll()
    {
        return _todoItems.OrderByDescending(t => t.CreatedDate).ToList();
    }
    
    public TodoItem? GetById(int id)
    {
        return _todoItems.FirstOrDefault(t => t.Id == id);
    }
    
    public void Create(TodoItem item)
    {
        item.Id = _nextId++;
        item.CreatedDate = DateTime.Now;
        _todoItems.Add(item);
    }
    
    public bool Update(TodoItem item)
    {
        var existingItem = GetById(item.Id);
        if (existingItem == null) return false;
        
        existingItem.Title = item.Title;
        existingItem.Description = item.Description;
        existingItem.IsCompleted = item.IsCompleted;
        
        return true;
    }
    
    public bool Delete(int id)
    {
        var item = GetById(id);
        if (item == null) return false;
        
        _todoItems.Remove(item);
        return true;
    }
    
    public bool ToggleComplete(int id)
    {
        var item = GetById(id);
        if (item == null) return false;
        
        item.IsCompleted = !item.IsCompleted;
        return true;
    }
}

using TodoDemo.Models;
using TodoDemo.Services;
using Xunit;

namespace TodoDemo.Tests;

public class TodoServiceTests
{
    [Fact]
    public void GetAll_ShouldReturnAllTodoItems()
    {
        // Arrange
        var service = new TodoService();

        // Act
        var result = service.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(result.Count >= 3); // Initial items
    }

    [Fact]
    public void GetById_WithValidId_ShouldReturnTodoItem()
    {
        // Arrange
        var service = new TodoService();
        var items = service.GetAll();
        var existingId = items.First().Id;

        // Act
        var result = service.GetById(existingId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingId, result.Id);
    }

    [Fact]
    public void GetById_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var service = new TodoService();

        // Act
        var result = service.GetById(99999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Create_ShouldAddNewTodoItem()
    {
        // Arrange
        var service = new TodoService();
        var initialCount = service.GetAll().Count;
        var newItem = new TodoItem
        {
            Title = "Test Item",
            Description = "Test Description",
            IsCompleted = false
        };

        // Act
        service.Create(newItem);
        var result = service.GetAll();

        // Assert
        Assert.Equal(initialCount + 1, result.Count);
        Assert.True(newItem.Id > 0);
        Assert.NotEqual(default(DateTime), newItem.CreatedDate);
    }

    [Fact]
    public void Update_WithValidId_ShouldUpdateTodoItem()
    {
        // Arrange
        var service = new TodoService();
        var existingItem = service.GetAll().First();
        var updatedItem = new TodoItem
        {
            Id = existingItem.Id,
            Title = "Updated Title",
            Description = "Updated Description",
            IsCompleted = true
        };

        // Act
        var result = service.Update(updatedItem);
        var item = service.GetById(existingItem.Id);

        // Assert
        Assert.True(result);
        Assert.NotNull(item);
        Assert.Equal("Updated Title", item.Title);
        Assert.Equal("Updated Description", item.Description);
        Assert.True(item.IsCompleted);
    }

    [Fact]
    public void Update_WithInvalidId_ShouldReturnFalse()
    {
        // Arrange
        var service = new TodoService();
        var updatedItem = new TodoItem
        {
            Id = 99999,
            Title = "Updated Title",
            Description = "Updated Description",
            IsCompleted = true
        };

        // Act
        var result = service.Update(updatedItem);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Delete_WithValidId_ShouldRemoveTodoItem()
    {
        // Arrange
        var service = new TodoService();
        var initialCount = service.GetAll().Count;
        var itemToDelete = service.GetAll().First();

        // Act
        var result = service.Delete(itemToDelete.Id);
        var finalCount = service.GetAll().Count;

        // Assert
        Assert.True(result);
        Assert.Equal(initialCount - 1, finalCount);
        Assert.Null(service.GetById(itemToDelete.Id));
    }

    [Fact]
    public void Delete_WithInvalidId_ShouldReturnFalse()
    {
        // Arrange
        var service = new TodoService();

        // Act
        var result = service.Delete(99999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ToggleComplete_WithValidId_ShouldToggleIsCompletedStatus()
    {
        // Arrange
        var service = new TodoService();
        var item = service.GetAll().First();
        var initialStatus = item.IsCompleted;

        // Act
        var result = service.ToggleComplete(item.Id);
        var updatedItem = service.GetById(item.Id);

        // Assert
        Assert.True(result);
        Assert.NotNull(updatedItem);
        Assert.Equal(!initialStatus, updatedItem.IsCompleted);
    }

    [Fact]
    public void ToggleComplete_WithInvalidId_ShouldReturnFalse()
    {
        // Arrange
        var service = new TodoService();

        // Act
        var result = service.ToggleComplete(99999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Create_MultipleTimes_ShouldAssignUniqueIds()
    {
        // Arrange
        var service = new TodoService();
        var item1 = new TodoItem { Title = "Item 1", IsCompleted = false };
        var item2 = new TodoItem { Title = "Item 2", IsCompleted = false };

        // Act
        service.Create(item1);
        service.Create(item2);

        // Assert
        Assert.NotEqual(item1.Id, item2.Id);
        Assert.True(item1.Id > 0);
        Assert.True(item2.Id > 0);
    }
}

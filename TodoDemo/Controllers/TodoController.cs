using Microsoft.AspNetCore.Mvc;
using TodoDemo.Models;
using TodoDemo.Services;

namespace TodoDemo.Controllers;

public class TodoController : Controller
{
    private readonly TodoService _todoService;

    public TodoController(TodoService todoService)
    {
        _todoService = todoService;
    }

    // GET: Todo
    public IActionResult Index()
    {
        var todos = _todoService.GetAll();
        return View(todos);
    }

    // GET: Todo/Details/5
    public IActionResult Details(int id)
    {
        var todo = _todoService.GetById(id);
        if (todo == null)
        {
            return NotFound();
        }
        return View(todo);
    }

    // GET: Todo/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Todo/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TodoItem todoItem)
    {
        if (ModelState.IsValid)
        {
            _todoService.Create(todoItem);
            TempData["SuccessMessage"] = "TODO item created successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(todoItem);
    }

    // GET: Todo/Edit/5
    public IActionResult Edit(int id)
    {
        var todo = _todoService.GetById(id);
        if (todo == null)
        {
            return NotFound();
        }
        return View(todo);
    }

    // POST: Todo/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, TodoItem todoItem)
    {
        if (id != todoItem.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var updated = _todoService.Update(todoItem);
            if (updated)
            {
                TempData["SuccessMessage"] = "TODO item updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        return View(todoItem);
    }

    // GET: Todo/Delete/5
    public IActionResult Delete(int id)
    {
        var todo = _todoService.GetById(id);
        if (todo == null)
        {
            return NotFound();
        }
        return View(todo);
    }

    // POST: Todo/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var deleted = _todoService.Delete(id);
        if (deleted)
        {
            TempData["SuccessMessage"] = "TODO item deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
        return NotFound();
    }

    // POST: Todo/ToggleComplete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ToggleComplete(int id)
    {
        var toggled = _todoService.ToggleComplete(id);
        if (toggled)
        {
            TempData["SuccessMessage"] = "TODO status updated!";
        }
        return RedirectToAction(nameof(Index));
    }
}

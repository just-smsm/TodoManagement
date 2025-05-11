using Application_Layer.IAppService;
using Domain_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using TodoManagement.ViewModels;

namespace TodoManagement.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: Todo
        public IActionResult Index(TodoStatus? status)
        {
            var todos = _todoService.GetTodos(status);
            ViewBag.Statuses = _todoService.GetTodoStatuses();
            ViewBag.SelectedStatus = status;
            return View(todos);
        }

        // GET: Todo/Create
        public IActionResult Create()
        {
            var viewModel = new TodoViewModel(); // Initialize a new empty TodoViewModel
            return View(viewModel);
        }

        // POST: Todo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TodoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var todo = new Todo
                    {
                        Id = Guid.NewGuid(),
                        Title = viewModel.Title,
                        Description = viewModel.Description, // This can be null or empty
                        Priority = viewModel.Priority,
                        Status = TodoStatus.Pending, // Default status
                        DueDate = viewModel.DueDate,
                        CreatedDate = DateTime.Now,
                        LastModifiedDate = DateTime.Now
                    };
                    _todoService.CreateTodo(todo);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(viewModel);
        }


        // GET: Todo/Edit/{id}
        public IActionResult Edit(Guid id)
        {
            var todo = _todoService.GetTodoById(id);
            if (todo == null)
            {
                return NotFound();
            }

            var viewModel = new TodoViewModel
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Priority = todo.Priority,
                Status = todo.Status,
                DueDate = todo.DueDate
            };

            return View(viewModel);
        }

        // POST: Todo/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TodoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var todo = _todoService.GetTodoById(viewModel.Id);
                    if (todo == null)
                    {
                        return NotFound();
                    }

                    todo.Title = viewModel.Title;
                    todo.Description = viewModel.Description;
                    todo.Priority = viewModel.Priority;
                    todo.Status = viewModel.Status;
                    todo.DueDate = viewModel.DueDate;
                    todo.LastModifiedDate = DateTime.Now;

                    _todoService.UpdateTodo(todo);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(viewModel);
        }

        // GET: Todo/Delete/{id}
        public IActionResult Delete(Guid id)
        {
            var todo = _todoService.GetTodoById(id);
            return View(todo);
        }

        // POST: Todo/DeleteConfirmed/{id}
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _todoService.DeleteTodo(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Todo/MarkComplete/{id}
        [HttpPost]
        public IActionResult MarkComplete(Guid id)
        {
            try
            {
                _todoService.MarkTodoAsComplete(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

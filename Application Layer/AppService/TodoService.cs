using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.IAppService;
using Application_Layer.ViewModels;
using Domain_Layer.Models;
using Infrastructure_Layer.Data;

namespace Application_Layer.AppService
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _context;

        public TodoService(TodoDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Todo> GetTodos(TodoStatus? status)
        {
            IQueryable<Todo> query = _context.Todos;

            if (status != null)
            {
                query = query.Where(t => t.Status == status);
            }

            // Ensure that the todos are ordered by CreatedDate descending.
            return query.OrderByDescending(t => t.CreatedDate).ToList();
        }

        public Todo GetTodoById(Guid id)
        {
            var todo = _context.Todos.Find(id);
            if (todo == null)
            {
                throw new KeyNotFoundException($"Todo with id {id} not found");
            }
            return todo;
        }

        public void CreateTodo(Todo todo)
        {
            // Input validation (moved from controller to service)
            if (string.IsNullOrWhiteSpace(todo.Title))
            {
                throw new ArgumentException("Title is required");
            }
            if (todo.Title.Length > 100)
            {
                throw new ArgumentException("Title cannot exceed 100 characters");
            }
            if (todo.DueDate.HasValue && todo.DueDate.Value < DateTime.UtcNow.Date)
            {
                throw new ArgumentException("Due date cannot be in the past");
            }

            todo.Id = Guid.NewGuid();
            todo.CreatedDate = DateTime.UtcNow;  // Set CreatedDate here
            todo.LastModifiedDate = DateTime.UtcNow; // Set LastModifiedDate here
            _context.Todos.Add(todo);
            _context.SaveChanges();
        }

        public void UpdateTodo(Todo todo)
        {
            // Input validation (moved from controller to service)
            if (string.IsNullOrWhiteSpace(todo.Title))
            {
                throw new ArgumentException("Title is required");
            }
            if (todo.Title.Length > 100)
            {
                throw new ArgumentException("Title cannot exceed 100 characters");
            }
            if (todo.DueDate.HasValue && todo.DueDate.Value < DateTime.UtcNow.Date)
            {
                throw new ArgumentException("Due date cannot be in the past");
            }
            var existingTodo = _context.Todos.Find(todo.Id);
            if (existingTodo == null)
            {
                throw new KeyNotFoundException($"Todo with id {todo.Id} not found");
            }

            existingTodo.Title = todo.Title;
            existingTodo.Description = todo.Description;
            existingTodo.Status = todo.Status;
            existingTodo.Priority = todo.Priority;
            existingTodo.DueDate = todo.DueDate;
            existingTodo.LastModifiedDate = DateTime.UtcNow; // Update LastModifiedDate
            _context.SaveChanges();
        }

        public void DeleteTodo(Guid id)
        {
            var todo = _context.Todos.Find(id);
            if (todo == null)
            {
                throw new KeyNotFoundException($"Todo with id {id} not found");
            }
            _context.Todos.Remove(todo);
            _context.SaveChanges();
        }

        public void MarkTodoAsComplete(Guid id)
        {
            var todo = _context.Todos.Find(id);
            if (todo == null)
            {
                throw new KeyNotFoundException($"Todo with id {id} not found");
            }
            todo.Status = TodoStatus.Completed;
            todo.LastModifiedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }

        //Get Todo Statuses
        public IEnumerable<TodoStatusViewModel> GetTodoStatuses()
        {
            return Enum.GetValues(typeof(TodoStatus))
                       .Cast<TodoStatus>()
                       .Select(s => new TodoStatusViewModel
                       {
                           Id = (int)s,
                           Name = s.ToString()
                       })
                       .ToList();
        }

        
    }
}

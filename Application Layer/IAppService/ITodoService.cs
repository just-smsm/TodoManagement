using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.ViewModels;
using Domain_Layer.Models;

namespace Application_Layer.IAppService
{
    public interface ITodoService
    {
        IEnumerable<Todo> GetTodos(TodoStatus? status);
        Todo GetTodoById(Guid id);
        void CreateTodo(Todo todo);
        void UpdateTodo(Todo todo);
        void DeleteTodo(Guid id);
        void MarkTodoAsComplete(Guid id);

        //Get Todo Statuses
        IEnumerable<TodoStatusViewModel> GetTodoStatuses();
    }
}

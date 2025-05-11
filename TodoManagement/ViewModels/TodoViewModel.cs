using System.ComponentModel.DataAnnotations;
using Domain_Layer.Models;

namespace TodoManagement.ViewModels
{
    public class TodoViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public TodoStatus Status { get; set; }

        [Required(ErrorMessage = "Priority is required")]
        public TodoPriority Priority { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
    }
}

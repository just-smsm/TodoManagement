using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Models
{
    public class Todo
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public TodoStatus Status { get; set; }

        public TodoPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; 

        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow; 
    }

}

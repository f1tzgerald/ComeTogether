using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Creator { get; set; }
        public DateTime DateAdded { get; set; }
        public string Text { get; set; }

        public int? ToDoItemId { get; set; }
        public TodoItem ToDoItem { get; set; }
    }
}

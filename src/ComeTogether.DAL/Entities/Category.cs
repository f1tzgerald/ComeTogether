using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [StringLength(25, MinimumLength = 1)]
        public string Name { get; set; }

        public ICollection<TodoItem> ToDoItems { get; set; }

        public List<CategoryPeople> CategoryPeople { get; set; }
    }
}

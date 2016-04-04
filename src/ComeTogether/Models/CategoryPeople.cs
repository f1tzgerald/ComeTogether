using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.Models
{
    public class CategoryPeople
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string UserId { get; set; }
        public Person Person { get; set; }
    }
}

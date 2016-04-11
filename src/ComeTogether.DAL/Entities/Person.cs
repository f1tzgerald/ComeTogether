using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ComeTogether.DAL.Entities
{
    public class Person : IdentityUser
    {
        public DateTime Created { get; set; }

        public List<CategoryPeople> CategoryPeople { get; set; }
    }
}

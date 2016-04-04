using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections;
using System.Collections.Generic;

namespace ComeTogether.Models
{
    public class Person : IdentityUser
    {
        public DateTime Created { get; set; }

        public List<CategoryPeople> CategoryPeople { get; set; }
    }
}
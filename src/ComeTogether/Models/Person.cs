using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ComeTogether.Models
{
    public class Person : IdentityUser
    {
        public DateTime Created { get; set; }
    }
}
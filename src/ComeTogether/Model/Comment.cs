using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Creator { get; set; }
        public DateTime DateAdded { get; set; }
        public string Text { get; set; }
    }
}

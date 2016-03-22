using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string Creator { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.UtcNow.Date;

        public string Text { get; set; }
    }
}

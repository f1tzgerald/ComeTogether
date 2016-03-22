using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.ViewModels
{
    public class ToDoItemViewModel
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow.Date;

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateFinish { get; set; }

        public string WhoDoIt { get; set; }
        public string Creator { get; set; }

        public bool Done { get; set; } = false;

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}

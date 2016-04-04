using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Added Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Finish Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateFinish { get; set; }

        [Display(Name = "Executor")]
        public string WhoDoIt { get; set; }
        public string  Creator { get; set; }
        public bool Done { get; set; }

        //public bool isDeleted { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

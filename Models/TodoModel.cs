using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoTaskApi2.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Description is required.")]
        [MinLength(3,ErrorMessage ="Minimum 3 characters allowed.")]
        [MaxLength(250,ErrorMessage ="Maximum 250 characters allowed.")]
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public int ColorId { get; set; }
        public string ColorCode { get; set; }
        public int Position { get; set; }
    }
}

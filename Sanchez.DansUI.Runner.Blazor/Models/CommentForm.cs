using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sanchez.DansUI.Runner.Blazor.Models
{
    public class CommentForm
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        public string Comment { get; set; }
    }
}


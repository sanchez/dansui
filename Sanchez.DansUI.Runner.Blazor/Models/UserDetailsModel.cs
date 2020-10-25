using System;
using System.ComponentModel.DataAnnotations;

namespace Sanchez.DansUI.Runner.Blazor.Models
{
    public class UserDetailsModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}

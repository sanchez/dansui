using System;
using System.ComponentModel.DataAnnotations;

namespace Sanchez.DansUI.Runner.Blazor.Models
{
    public class AddressInfo
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string StreetName { get; set; }

        [Required]
        public string State { get; set; }
    }
}

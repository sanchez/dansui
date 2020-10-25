using System;
using System.ComponentModel.DataAnnotations;

namespace Sanchez.DansUI.Runner.Blazor.Models
{
    public class ProductRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public ProductInformation RequestedProduct { get; set; }
    }
}

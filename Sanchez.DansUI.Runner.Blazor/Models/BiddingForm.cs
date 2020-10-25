using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sanchez.DansUI.Runner.Blazor.Models
{
    public class BiddingForm
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Range(5, 50, ErrorMessage = "Range must be between 5 and 50")]
        public int BidAmount { get; set; }

        public bool EmailUpdates { get; set; } = true;

        [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree to the terms")]
        public bool AgreeToTerms { get; set; }
    }
}


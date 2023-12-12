using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IstanbulLMC.Models
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "First name")]
        public string firstName { get; set; }
        [Display(Name = "Last name")]
        public string lastName { get; set; }

    }
}

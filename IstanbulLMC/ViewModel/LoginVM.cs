using System.ComponentModel.DataAnnotations;

namespace IstanbulLMC.ViewModel
{
    public class LoginVM
    {

        [Required(ErrorMessage = "Email Address is Invalid")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Invalid")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

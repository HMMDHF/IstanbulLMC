using System.ComponentModel.DataAnnotations;

namespace IstanbulLMC.ViewModel
{
    public class ResetPass
    {
        [Required(ErrorMessage = "Password is Invalid")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Pass")]
        public string ConfirmPass { get; set; }
    }
}
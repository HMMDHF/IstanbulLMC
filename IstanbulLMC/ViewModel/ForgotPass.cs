using System.ComponentModel.DataAnnotations;

namespace IstanbulLMC.ViewModel
{
    public class ForgotPass
    {

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Tag is required")]
        public string Mail { get; set; }
    }
}

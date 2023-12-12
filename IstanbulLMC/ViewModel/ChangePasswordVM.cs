using System.ComponentModel.DataAnnotations;

namespace IstanbulLMC.ViewModel
{
    public class ChangePasswordVM
    {
        [Required]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }
        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]

        public string NewPassword { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}

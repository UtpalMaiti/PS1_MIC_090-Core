using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PS1_MIC_090_Core.Models
{
    public class LogOnViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "UserName is required")]
        [StringLength(10, ErrorMessage = "UserName should not exceed more than 10 characters")]
        [Remote("ValidateName", "Home")]
        [DisplayName("Username:")]
        public required string UserName { get; set; }
        [DisplayName("Password:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "password should not exceed more than 20 characters")]
        public required string Password { get; set; }
        [DisplayName("Remember Me ?")]
        public bool isRememberMe { get; set; }
        //[Range(typeof(bool), "true", "false", ErrorMessage = "Please Accept the Terms and Conditions")]
        //public bool isAcceptTermsAndConditions { get; set; } = true;
    }
}

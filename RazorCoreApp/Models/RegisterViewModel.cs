using System.ComponentModel;

namespace PS1_MIC_090_Core.Models
{
    public class RegisterViewModel
    {
        [DisplayName("Username:")]
        public required string UserName { get; set; }
        [DisplayName("Email:")]
        public required string Email { get; set; }
        [DisplayName("Password:")]
        public required string Password { get; set; }
        [DisplayName("Confirm Password")]
        public required string ConfirmPassword { get; set; }
    }
}


using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using PS1_MIC_090_Core.Repository;

namespace PS1_MIC_090_Core.Models.Extensions
{
 

    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IUserRepository _userRepository;

        public CustomCookieAuthenticationEvents(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;

            // Look for the LastChanged claim.
            var lastChanged = (from c in userPrincipal!.Claims
                               where c.Type == "LastChanged"
                               select c.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(lastChanged) ||
                !_userRepository.ValidateLastChanged(lastChanged))
            {
                context.RejectPrincipal();

                await context.HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}

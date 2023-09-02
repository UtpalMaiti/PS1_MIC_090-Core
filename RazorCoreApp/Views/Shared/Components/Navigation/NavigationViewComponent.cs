using Microsoft.AspNetCore.Mvc;

using PS1_MIC_090_Core.Models;
using PS1_MIC_090_Core.Repository;
using PS1_MIC_090_Core.Repository.Contracts;
using PS1_MIC_090_Core.Repository.Domain;

namespace PS1_MIC_090_Core.Views.Shared.Components.Navigation
{
    public class NavigationViewComponent : ALLViewComponent
    {
        private readonly IUserRepository userRepository;
        private readonly IRepository<Role> roles;
        private readonly IRepository<UserRoleMapping> userRoleMappings;

        public NavigationViewComponent(
            IUserRepository userRepository,
            IRepository<Role> roles,
            IRepository<UserRoleMapping> userRoleMappings
            )
        {
            this.userRepository = userRepository;
            this.roles = roles;
            this.userRoleMappings = userRoleMappings;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeaderViewModel model = new HeaderViewModel();
            int userid = GetCurrentUserId();

            var user = (await this.userRepository.GetAll())
                .FirstOrDefault(x => x.UserId == userid);

            if (user != null)
            {
                model.UserName = user.UserName;
                model.Email = user.EmailId;

                var mapping = (await this.userRoleMappings.GetAll())
                    .FirstOrDefault(x => x.UserId == userid);
                if (mapping != null)
                {
                    var role = (await this.roles.GetAll())
                  .FirstOrDefault(x => x.RoleId == mapping.RoleId);

                    if (role != null)
                    {
                        model.RoleName = role.RoleName;
                    }
                }
            }
            model.CompanyName = "Company Logo";

            return  View(model);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace PS1_MIC_090_Core.Views.Shared.Components.Navigation
{
    public class NavigationViewComponent : ALLViewComponent
    {
        public NavigationViewComponent()
        {
        }
        public IViewComponentResult Invoke()
        {
       

            return View();
        }
    }
}

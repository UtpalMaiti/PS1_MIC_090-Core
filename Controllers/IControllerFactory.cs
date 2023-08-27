using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Azure;

namespace PS1_MIC_090_Core.Controllers
{
    public class DefaultControllerFactory : IControllerFactory
    {
        private readonly IControllerActivator _controllerActivator;

        public DefaultControllerFactory(IControllerActivator controllerActivator)
        {
            _controllerActivator = controllerActivator;
        }

        public object CreateController(ControllerContext context)
        {
            return _controllerActivator.Create(context);
        }

        public void ReleaseController(ControllerContext context, object controller)
        {
            _controllerActivator.Release(context, controller);
        }
    }


}

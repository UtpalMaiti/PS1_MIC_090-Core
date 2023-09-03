using BlazorApp.Client.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Services
{
    public class ComponentBase : IComponent
    {
        [Inject]
        protected IDataAccess DataRepository { get; set; } = default!;

        public void Attach(RenderHandle renderHandle)
        {

        }

        public async Task SetParametersAsync(ParameterView parameters)
        {
            await Task.FromResult(true);
        }
    }
}
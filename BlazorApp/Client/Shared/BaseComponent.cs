using BlazorApp.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorApp.Client.Services
{
    public class BaseComponent : ComponentBase,IComponent, IHandleEvent, IHandleAfterRender
    {
        public BaseComponent()
        {
        }

        [Inject]
        protected IDataAccess DataRepository { get; set; } = default!;

        public void Attach(RenderHandle renderHandle)
        {

        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            return base.SetParametersAsync(parameters);
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }

        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }

        protected override bool ShouldRender()
        {
            return base.ShouldRender();
        }
    }
}
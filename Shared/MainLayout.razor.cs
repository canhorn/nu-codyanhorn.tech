namespace CodyAnhorn.Tech.Shared
{
    using System.Reflection;
    using Microsoft.AspNetCore.Components;

    public class MainLayoutBase
        : LayoutComponentBase
    {
        [CascadingParameter(Name = "IsPreview")]
        public bool IsPreview { get; set; }
        public string AnimateCSS { get; set; } = string.Empty;

        public string Pathname { get; set; } = "/";

        protected override void OnParametersSet()
        {
            Pathname = (Body?.Target as RouteView)
                ?.RouteData
                .PageType
                ?.GetCustomAttribute<RouteAttribute>()
                ?.Template ?? "/";
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            AnimateCSS = "--animate";
        }
    }
}

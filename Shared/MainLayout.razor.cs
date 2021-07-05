namespace CodyAnhorn.Tech.Shared
{
    using System.Reflection;
    using CodyAnhorn.Tech.Localization;
    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Localization;

    public class MainLayoutBase
        : LayoutComponentBase
    {
        [CascadingParameter(Name = "IsPreview")]
        public bool IsPreview { get; set; }

        [Inject]
        public IStringLocalizer<LocalizationResource> Localizer { get; set; } = null!;

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

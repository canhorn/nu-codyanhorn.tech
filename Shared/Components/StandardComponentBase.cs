namespace CodyAnhorn.Tech.Shared.Components
{
    using CodyAnhorn.Tech.Localization;
    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Localization;

    public class StandardComponentBase
        : ComponentBase
    {
        [Inject]
        public IStringLocalizer<LocalizationResource> Localizer { get; set; } = null!;
    }
}

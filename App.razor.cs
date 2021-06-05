namespace CodyAnhorn.Tech
{
    using CodyAnhorn.Tech.Data;
    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Options;

    public class AppBase
        : ComponentBase
    {
        [Inject]
        IOptions<SiteConfig> SiteConfigOptions { get; set; } = null!;

        public SiteConfig SiteConfig { get; set; } = new SiteConfig();

        protected override void OnInitialized()
        {
            SiteConfig = SiteConfigOptions.Value;
        }
    }
}

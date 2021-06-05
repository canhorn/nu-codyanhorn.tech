namespace CodyAnhorn.Tech.Pages
{
    using System.Threading.Tasks;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Page;
    using CodyAnhorn.Tech.Data;
    using CodyAnhorn.Tech.Shared.Components;
    using Microsoft.AspNetCore.Components;

    public class IndexBase
        : StandardComponentBase
    {
        [CascadingParameter]
        public SiteConfig Config { get; set; } = null!;

        [Inject]
        public ContentfulSdk.Api.ContentfulApi ContentfulApi { get; set; } = null!;

        public PageContent PageContent { get; set; } = new PageContent();


        protected override async Task OnInitializedAsync()
        {
            var result = await ContentfulApi.GetPageContentBySlug(
                Config.PageMeta.Home.Slug
            );
            if (result is not null)
            {
                PageContent = result;
            }
        }

    }
}

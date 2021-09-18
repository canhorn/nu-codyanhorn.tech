namespace CodyAnhorn.Tech.Pages.Uses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Page;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Uses;
    using CodyAnhorn.Tech.Data;
    using CodyAnhorn.Tech.Shared.Components;
    using Microsoft.AspNetCore.Components;

    public class UsesBase
        : StandardComponentBase
    {
        [CascadingParameter]
        public SiteConfig Config { get; set; } = null!;

        [Inject]
        public ContentfulSdk.Api.ContentfulApi ContentfulApi { get; set; } = null!;

        public PageContent PageContent { get; set; } = new PageContent();
        protected IEnumerable<UsesEntry> UsesEntries { get; private set; } = new List<UsesEntry>();


        protected override async Task OnInitializedAsync()
        {
            var result = await ContentfulApi.GetPageContentBySlug(
                Config.PageMeta.UsesIndex.Slug
            );
            if (result is not null)
            {
                PageContent = result;
            }

            UsesEntries = await ContentfulApi.GetAllUsesEntries();
        }

    }
}

namespace CodyAnhorn.Tech.Pages.Uses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Page;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Uses;
    using CodyAnhorn.Tech.Data;
    using CodyAnhorn.Tech.Shared.Components;
    using Microsoft.AspNetCore.Components;

    public class UsesSubPageBase
        : StandardComponentBase
    {
        [CascadingParameter]
        public SiteConfig Config { get; set; } = null!;
        [Parameter]
        public string? Identifier { get; set; }

        [Inject]
        public ContentfulSdk.Api.ContentfulApi ContentfulApi { get; set; } = null!;

        public PageContent PageContent { get; set; } = new PageContent();
        protected IEnumerable<UsesEntry> UsesEntries { get; private set; } = new List<UsesEntry>();


        protected override async Task OnInitializedAsync()
        {
            await Setup();
        }

        protected override async Task OnParametersSetAsync()
        {
            await Setup();
        }

        private async Task Setup()
        {
            var slug = Config.PageMeta.UsesIndex.Slug;
            if (Identifier.IsNotNullOrWhitespace())
            {
                slug = $"/uses/{Identifier}";
            }

            var result = await ContentfulApi.GetPageContentBySlug(
                slug
            );
            if (result is not null)
            {
                PageContent = result;
            }

            UsesEntries = await ContentfulApi.GetAllUsesEntries();
        }
    }
}

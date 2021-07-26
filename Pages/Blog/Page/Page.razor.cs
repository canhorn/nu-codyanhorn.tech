namespace CodyAnhorn.Tech.Pages.Blog.Page
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Blog;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Page;
    using CodyAnhorn.Tech.Data;
    using CodyAnhorn.Tech.Shared.Components;
    using Microsoft.AspNetCore.Components;

    public class PageBase
        : StandardComponentBase
    {
        [CascadingParameter]
        public SiteConfig Config { get; set; } = null!;

        [Parameter]
        public int Page { get; set; }

        [Inject]
        public ContentfulSdk.Api.ContentfulApi ContentfulApi { get; set; } = null!;

        public PageContent PageContent { get; set; } = new PageContent();

        public int TotalPages { get; set; }
        public IEnumerable<BlogPost> PostSummaries { get; set; } = new List<BlogPost>();
        public int CurrentPage { get; set; } = 1;

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
            var result = await ContentfulApi.GetPageContentBySlug(
                Config.PageMeta.BlogIndex.Slug
            );
            if (result is not null)
            {
                PageContent = result;
            }
            var (Total, Items) = await ContentfulApi.GetPaginatedPostSummaries(
                Page
            );
            TotalPages = (int)Math.Ceiling(
                decimal.Divide(Total, Config.Pagination.PageSize)
            );
            PostSummaries = Items;
            CurrentPage = Page;
        }
    }
}

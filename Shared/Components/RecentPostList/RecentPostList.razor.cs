namespace CodyAnhorn.Tech.Shared.Components.RecentPostList
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CodyAnhorn.Tech.ContentfulSdk.Api;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Blog;
    using CodyAnhorn.Tech.Data;
    using Microsoft.AspNetCore.Components;

    public class RecentPostListBase
        : StandardComponentBase
    {
        [CascadingParameter]
        public SiteConfig Config { get; set; } = null!;

        [Inject]
        public ContentfulApi ContentfulApi { get; set; } = null!;

        public IEnumerable<BlogPost> Posts { get; set; } = new List<BlogPost>();

        protected override async Task OnInitializedAsync()
        {
            Posts = await ContentfulApi.GetRecentPostList();
        }

    }
}

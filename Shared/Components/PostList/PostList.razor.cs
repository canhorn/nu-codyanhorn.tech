namespace CodyAnhorn.Tech.Shared.Components.PostList
{
    using System.Collections.Generic;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Blog;
    using CodyAnhorn.Tech.Data;
    using Microsoft.AspNetCore.Components;

    public class PostListBase
        : StandardComponentBase
    {
        [CascadingParameter]
        public SiteConfig Config { get; set; } = null!;

        [Parameter]
        public IEnumerable<BlogPost> Posts { get; set; } = new List<BlogPost>();
        [Parameter]
        public int CurrentPage { get; set; }
        [Parameter]
        public int TotalPages { get; set; }


        public bool NextDisabled => CurrentPage == TotalPages;
        public bool PrevDisabled => CurrentPage == 1;
    }
}

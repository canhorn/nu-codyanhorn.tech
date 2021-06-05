namespace CodyAnhorn.Tech.Shared.Components.Post
{
    using System.Reflection.Metadata;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Blog;
    using Microsoft.AspNetCore.Components;

    public class PostBase
        : StandardComponentBase
    {
        [Parameter]
        public BlogPost Post { get; set; } = null!;
    }
}

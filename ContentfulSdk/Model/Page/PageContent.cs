namespace CodyAnhorn.Tech.ContentfulSdk.Model.Page
{
    using Contentful.Core.Models;

    public class PageContent
    {
        public SystemProperties Sys { get; set; } = new SystemProperties();
        public PageHeroBanner? HeroBanner { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public bool IsPlatformPage { get; set; }
        public PageBody? Body { get; set; }
        public string? MarkdownBody { get; set; }
    }
}

namespace CodyAnhorn.Tech.ContentfulSdk.Model.Blog
{
    using System.Collections.Generic;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Page;
    using Contentful.Core.Models;

    public class BlogPost
    {
        public SystemProperties Sys { get; set; } = new SystemProperties();
        public string Date { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Excerpt { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new List<string>();
        public string ExternalUrl { get; set; } = string.Empty;
        public Person? Author { get; set; } 

        public PageBody? Body { get; set; }
        public string? MarkdownBody { get; set; }

        public static string GenerateSlug(
            string rootSlug,
            string slug
        )
        {
            if (slug.StartsWith("/"))
            {
                return slug;
            }

            return $"{rootSlug}/{slug}";
        }
    }
}

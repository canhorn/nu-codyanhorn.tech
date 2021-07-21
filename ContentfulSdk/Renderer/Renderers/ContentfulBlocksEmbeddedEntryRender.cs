namespace CodyAnhorn.Tech.ContentfulSdk.Renderer.Renderers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Blog;
    using Contentful.Core.Models;

    public class ContentfulBlocksEmbeddedEntryRender
        : IContentRenderer
    {
        private readonly string _blogRootSlug;

        public ContentfulBlocksEmbeddedEntryRender(string blogRootSlug)
        {
            _blogRootSlug = blogRootSlug;
        }

        public int Order
        {
            get;
            set;
        } = 100;

        public bool SupportsContent(IContent content)
        {
            return content is EntryStructure;
        }

        public string Render(IContent content)
        {
            var entryStructure = content as EntryStructure;
            var stringBuilder = new StringBuilder();
            if (entryStructure?.Data.Target is not CustomNode target)
            {
                return string.Empty;
            }

            var targetTyped = target.JObject.ToObject<CustomNodeTyped>();
            var contentType = targetTyped.Sys.ContentType.SystemProperties.Id;

            switch (contentType)
            {
                case "blogPost":
                    var blogHref = BlogPost.GenerateSlug(
                        _blogRootSlug,targetTyped.Slug
                    );
                    var blogTitle = targetTyped.Excerpt;
                    var blogContent = targetTyped.Title;
                    stringBuilder.Append("<a class=\"blog-post-hyperlink\"")
                        .Append(" href=\"").Append(blogHref).Append('"')
                        .Append(" title=\"").Append(blogTitle).Append('"');

                    stringBuilder.Append('>')
                        .Append(blogContent)
                        .Append("</a>");
                    break;
                case "videoEmbed":
                    var title = targetTyped.Title;
                    var embedUrl = targetTyped.EmbedUrl;
                    stringBuilder.Append("<div class=\"video-embed\"><iframe class=\"video-embed__iframe\" ")
                        .Append(" src=\"").Append(embedUrl).Append('"')
                        .Append(" height=\"100%\"")
                        .Append(" width=\"100%\"")
                        .Append(" frameBoarder=\"0\"")
                        .Append(" scrolling=\"no\"")
                        .Append(" allowFullScreen=\"true\"")
                        .Append(" title=\"").Append(title).Append('"')
                        .Append("></iframe></div>");
                    break;
                case "codeBlock":
                    var language = targetTyped.Language;
                    var code = targetTyped.Code;
                    stringBuilder.Append("<pre class=\"code-block\"><code class=\"code-block__inner ")
                        .Append("language-").Append(language)
                        .Append("\">")
                        .Append(WebUtility.HtmlEncode(code))
                        .Append("</code></pre>");
                    break;
                case "hyperlink":
                    var href = targetTyped.Href;
                    var linkTitle = targetTyped.Title;
                    var linkRel = targetTyped.Rel;
                    var linkTarget = targetTyped.Target;
                    var linkContent = targetTyped.Content;
                    stringBuilder.Append("<a class=\"hyperlink\"")
                        .Append(" href=\"").Append(href).Append('"')
                        .Append(" title=\"").Append(linkTitle).Append('"');

                    if (linkRel.IsNotNullOrWhitespace())
                    {
                        stringBuilder.Append(" rel=\"").Append(linkRel).Append('"');
                    }

                    if (linkTarget.IsNotNullOrWhitespace())
                    {
                        stringBuilder.Append(" target=\"").Append(linkTarget).Append('"');
                    }

                    stringBuilder.Append('>')
                        .Append(linkContent)
                        .Append("</a>");
                    break;
                default:
                    break;
            }

            return stringBuilder.ToString();
        }

        public Task<string> RenderAsync(IContent content)
        {
            return Task.FromResult(Render(content));
        }

        public class CustomNodeTyped
        {
            public SystemProperties Sys { get; set; } = new SystemProperties();
            public string Title { get; set; } = string.Empty;
            public string EmbedUrl { get; set; } = string.Empty;
            public string Language { get; set; } = string.Empty;
            public string Code { get; set; } = string.Empty;
            public string Href { get; set; } = string.Empty;
            public string Rel { get; set; } = string.Empty;
            public string Target { get; set; } = string.Empty;
            public string Content { get; set; } = string.Empty;

            public string Slug { get; set; } = string.Empty;
            public string Excerpt { get; set; } = string.Empty;
            public List<string> Categories { get; set; } = new();
            public List<string> Tags { get; set; } = new();
        }
    }
}

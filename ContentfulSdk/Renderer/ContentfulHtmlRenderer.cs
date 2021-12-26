namespace CodyAnhorn.Tech.ContentfulSdk.Renderer
{
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Page;
    using CodyAnhorn.Tech.ContentfulSdk.Renderer.Renderers;
    using CodyAnhorn.Tech.Data;
    using Contentful.Core.Models;
    using Microsoft.Extensions.Options;

    public class ContentfulHtmlRenderer
    {
        private readonly HtmlRenderer _renderer;

        public ContentfulHtmlRenderer(
            IOptions<SiteConfig> options
        )
        {
            _renderer = new HtmlRenderer();
            _renderer.AddRenderer(
                new ContentfulBlocksEmbeddedEntryRender(
                    options.Value.PageMeta.BlogIndex.Slug
                )
            );
            _renderer.AddRenderer(
                new ContentfulLazyImageRender()
            );
            _renderer.AddRenderer(
                // Run this one first to not encode the HTML in text.
                new TextRenderer(htmlEncodeOutput: false)
                {
                    Order = 50,
                }
            );
        }

        public async Task<string> Render(
            PageBody body
        ) => await _renderer.ToHtml(
            body
        );
    }
}

namespace CodyAnhorn.Tech.ContentfulSdk.Renderer
{
    using System.Threading.Tasks;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Page;
    using CodyAnhorn.Tech.ContentfulSdk.Renderer.Renderers;
    using Contentful.Core.Models;

    public class ContentfulHtmlRenderer
    {
        private readonly HtmlRenderer _renderer;

        public ContentfulHtmlRenderer()
        {
            _renderer = new HtmlRenderer();
            _renderer.AddRenderer(
                new ContentfulBlocksEmbeddedEntryRender()
            );
            _renderer.AddRenderer(
                new ContentfulLazyImageRender()
            );
        }

        public async Task<string> Render(
            PageBody body
        ) => await _renderer.ToHtml(
            body
        );
    }
}

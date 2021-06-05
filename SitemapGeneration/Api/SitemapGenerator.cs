namespace CodyAnhorn.Tech.SitemapGeneration.Api
{
    using System.Threading.Tasks;
    using CodyAnhorn.Tech.CacheBusting.Api;

    public interface SitemapGenerator
        : BustCache
    {
        Task<string> Generate();
    }
}

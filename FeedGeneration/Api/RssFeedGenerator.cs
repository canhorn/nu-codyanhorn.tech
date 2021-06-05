namespace CodyAnhorn.Tech.FeedGeneration.Api
{
    using System.Threading.Tasks;
    using CodyAnhorn.Tech.CacheBusting.Api;

    public interface RssFeedGenerator
        : CacheBuster
    {
        Task<string> Generate();
    }
}

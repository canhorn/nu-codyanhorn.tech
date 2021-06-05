namespace CodyAnhorn.Tech.CacheBusting.Api
{
    using System.Threading.Tasks;

    public interface BustCache
    {
        int Order { get; }
        Task<bool> BustCache();
    }
}

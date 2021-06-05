namespace CodyAnhorn.Tech.CacheBusting.Api
{
    using System.Threading.Tasks;

    public interface CacheBuster
    {
        Task<bool> BustCache();
    }
}

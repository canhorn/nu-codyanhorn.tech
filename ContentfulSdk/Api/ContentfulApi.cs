namespace CodyAnhorn.Tech.ContentfulSdk.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CodyAnhorn.Tech.CacheBusting.Api;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Blog;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Page;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Uses;

    public interface ContentfulApi
        : BustCache
    {
        Task<PageContent?> GetPageContentBySlug(
            string slug
        );

        Task<IEnumerable<PageContent>> GetNonPlatformContentPages();

        Task<IEnumerable<UsesEntry>> GetAllUsesEntries();

        Task<(int Total, IEnumerable<BlogPost> Items)> GetPaginatedBlogPosts(
            int page
        );

        Task<IEnumerable<string>> GetAllPostSlugs();

        Task<IEnumerable<BlogPost>> GetAllBlogPosts();

        Task<IEnumerable<BlogPost>> GetAllCachedBlogPosts();

        Task<BlogPost?> GetPostBySlug(
            string slug
        );

        Task<(int Total, IEnumerable<BlogPost> Items)> GetPaginatedPostSummaries(
            int page
        );

        Task<IEnumerable<BlogPost>> GetRecentPostList();

        Task GetTotalPostsNumber();
    }
}

namespace CodyAnhorn.Tech.ContentfulSdk.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CodyAnhorn.Tech.CacheBusting.Api;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Blog;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Page;

    public interface ContentfulApi
        : BustCache
    {
        Task<PageContent?> GetPageContentBySlug(
            string slug
        );

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

namespace CodyAnhorn.Tech.Pages.Blog
{
    using System.Threading.Tasks;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Blog;
    using CodyAnhorn.Tech.Data;
    using CodyAnhorn.Tech.Shared.Components;
    using Microsoft.AspNetCore.Components;

    public class PostPageBase
        : StandardComponentBase
    {
        [CascadingParameter]
        public SiteConfig Config { get; set; } = null!;

        [Parameter]
        public string Slug { get; set; } = string.Empty;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ContentfulSdk.Api.ContentfulApi ContentfulApi { get; set; } = null!;

        public BlogPost? Post { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadPage();
        }

        protected override async Task OnParametersSetAsync()
        {
            await LoadPage();
        }

        private async Task LoadPage()
        {
            if (Slug == Post?.Slug)
            {
                return;
            }

            var postResult = await ContentfulApi.GetPostBySlug(
               Slug
            );
            if (postResult is null)
            {
                NavigationManager.NavigateTo("/not-found");
                return;
            }

            Post = postResult;
        }

    }
}

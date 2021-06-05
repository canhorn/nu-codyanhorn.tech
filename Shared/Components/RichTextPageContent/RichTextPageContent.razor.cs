﻿namespace CodyAnhorn.Tech.Shared.Components.RichTextPageContent
{
    using System.Threading.Tasks;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Page;
    using CodyAnhorn.Tech.ContentfulSdk.Renderer;
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;

    public partial class RichTextPageContent
        : ComponentBase
    {
        [Parameter]
        public PageBody? Body { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; } = null!;
        [Inject]
        public ContentfulHtmlRenderer Renderer { get; set; } = null!;

        public string Content { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (Body is not null)
            {
                Content = await Renderer.Render(
                    Body
                );
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync(
                    "Prisim_highlightAll"
                );
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Body is not null)
            {
                Content = await Renderer.Render(
                    Body
                );
            }
        }
    }
}

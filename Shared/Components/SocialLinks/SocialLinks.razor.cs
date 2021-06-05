namespace CodyAnhorn.Tech.Shared.Components.SocialLinks
{
    using System;
    using System.Collections.Generic;
    using CodyAnhorn.Tech.Data;
    using CodyAnhorn.Tech.Shared.Components.SocialLinks.svg;
    using Microsoft.AspNetCore.Components;

    public class SocialLinksBase
        : StandardComponentBase
    {
        [CascadingParameter]
        public SiteConfig Config { get; set; } = null!;

        [Parameter]
        public string FillColor { get; set; } = "inherit";

        public List<SocialLinkDetails> SocialLinkList = new();

        protected override void OnInitialized()
        {
            SocialLinkList = new List<SocialLinkDetails>
            {
                new SocialLinkDetails
                {
                    Name = Localizer["Twitch"]!,
                    Url = $"https://Twitch.tv/{Config.PageMeta.OpenGraph.TwitchUser}",
                    AriaLabel = Localizer["Checkout Cody Anhorn's Twitch Channel"]!,
                    SVG = typeof(Twitch),
                },

                new SocialLinkDetails
                {
                    Name = Localizer["Twitter"]!,
                    Url = $"https://twitter.com/{Config.PageMeta.OpenGraph.TwitterUser}",
                    AriaLabel = Localizer["Checkout Cody Anhorn's Twitter"]!,
                    SVG = typeof(Twitter),
                },

                new SocialLinkDetails
                {
                    Name = Localizer["Discord"]!,
                    Url = $"https://discord.gg/{Config.PageMeta.OpenGraph.DiscordUrl}",
                    AriaLabel = Localizer["Checkout Cody Anhorn's Discord Server"]!,
                    SVG = typeof(Discord),
                },

                new SocialLinkDetails
                {
                    Name = Localizer["GitHub"]!,
                    Url = $"https://github.com/{Config.PageMeta.OpenGraph.GitHubUser}",
                    AriaLabel = Localizer["Checkout Cody Anhorn's GitHub"]!,
                    SVG = typeof(GitHub),
                },
                new SocialLinkDetails
                {
                    Name = Localizer["RSS Feed"]!,
                    Url = "feed.xml",
                    AriaLabel = Localizer["View the RSS feed of {0}", Config.Site.Domain]!,
                    SVG = typeof(Feed),
                }
            };
        }

        public struct SocialLinkDetails
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public string AriaLabel { get; set; }
            public Type SVG { get; set; }
        }
    }
}

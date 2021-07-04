using Microsoft.AspNetCore.WebUtilities;

namespace CodyAnhorn.Tech.Data
{
    public class OpenGraph
    {
        public string TwitterUser { get; set; } = string.Empty;
        public string TwitchUser { get; set; } = string.Empty;
        public string GitHubUser { get; set; } = string.Empty;
        public string DiscordUrl { get; set; } = string.Empty;


        public static string GenerateImageUrl(
            string heading,
            string title
        )
        {
            // Here we are using a Vercel application to generate a dynamic Image for our Open Graph tags.
            return string.Join(
                string.Empty,
                "https://cody-anhorn-tech-og-image.vercel.app/",
                title,
                ".png?theme=dark&md=0fontSize=100px",
                "&heading=",
                heading,
                "&images=https%3A%2F%2Fcody-anhorn-tech-og-image.vercel.app%2Flogo.png"
            );
        }
    }
}
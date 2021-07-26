namespace CodyAnhorn.Tech.RobotsTxtGeneration.Generator
{
    using CodyAnhorn.Tech.Data;
    using CodyAnhorn.Tech.RobotsTxtGeneration.Api;
    using Microsoft.Extensions.Options;

    public class StaticRobotsTxtGenerator
        : RobotsTxtGenerator
    {
        private readonly string Cached_TXT = string.Empty;

        public int Order => 10;

        public StaticRobotsTxtGenerator(
            IOptions<SiteConfig> siteConfigOptions
        )
        {
            var siteConfig = siteConfigOptions.Value;
            Cached_TXT = GenerateTxt(
                siteConfig
            );
        }

        public string Generate()
        {
            return Cached_TXT;
        }

        private static string GenerateTxt(
            SiteConfig siteConfig
        )
        {
            if (siteConfig.Site.DisallowRobotsAccess)
            {
                return @"
User-agent: *
Disallow: /
";
            }

            var siteurl = siteConfig.SITE_URL;
            return $@"
# *
User-agent: *
Allow: /

# Host
Host: {siteurl}

# Sitemaps
Sitemap: {siteurl}/sitemap.xml
";
        }
    }
}

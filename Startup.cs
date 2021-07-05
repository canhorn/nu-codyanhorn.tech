namespace CodyAnhorn.Tech
{
    using System.Collections.Generic;
    using System.Globalization;
    using CodyAnhorn.Tech.CacheBusting.Api;
    using CodyAnhorn.Tech.CacheBusting.Manual;
    using CodyAnhorn.Tech.ContentfulSdk.Api;
    using CodyAnhorn.Tech.ContentfulSdk.Renderer;
    using CodyAnhorn.Tech.ContentfulSdk.Sdk;
    using CodyAnhorn.Tech.Data;
    using CodyAnhorn.Tech.FeedGeneration.Api;
    using CodyAnhorn.Tech.FeedGeneration.Generators;
    using CodyAnhorn.Tech.Localization;
    using CodyAnhorn.Tech.PageMetadataGeneration.Api;
    using CodyAnhorn.Tech.PageMetadataGeneration.Genereators;
    using CodyAnhorn.Tech.RobotsTxtGeneration.Api;
    using CodyAnhorn.Tech.RobotsTxtGeneration.Generator;
    using CodyAnhorn.Tech.SitemapGeneration.Api;
    using CodyAnhorn.Tech.SitemapGeneration.Generators;
    using Contentful.AspNetCore;
    using EventHorizon.Blazor.DocumentMetadata;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Localization;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();


            services.AddTransient<PageMetadataGenerator, StaticPageMetadataGenerator>()
                .AddDocumentMetadata(async (serviceProvider, registrator) =>
                {
                    var localizer = serviceProvider.GetRequiredService<IStringLocalizer<LocalizationResource>>();
                    var generator = serviceProvider.GetRequiredService<PageMetadataGenerator>();
                    await generator.Generate(
                        registrator,
                        localizer
                    );
                });

            // Setup Sitemap Services
            services.AddSingleton<SitemapGenerator, DynamicSitemapGenerator>();

            // Setup RSS Feed Services
            services.AddSingleton<RssFeedGenerator, ContentfulRssFeedGenerator>();

            // Setup robots.txt Services
            services.AddSingleton<RobotsTxtGenerator, StaticRobotsTxtGenerator>();

            // I18n Services
            // This registers the supported Locales for localization.
            // As more platform locales are supported, Localization Resource files can be added in 
            // ~/Resources/Localization/LocalizationResource-**.resx. 
            services
                .AddLocalization(options => options.ResourcesPath = "Resources")
                .Configure<RequestLocalizationOptions>(
                    opts =>
                    {
                        var supportedCultures = new List<CultureInfo>
                        {
                            // Set Supported Locales
                            new CultureInfo("en-US"),
                        };

                        opts.DefaultRequestCulture = new RequestCulture("en-US");
                        // Formatting numbers, dates, etc.
                        opts.SupportedCultures = supportedCultures;
                        // UI strings that we have localized.
                        opts.SupportedUICultures = supportedCultures;
                    }
                );

            // Setup Site and Contentful Services
            services.Configure<SiteConfig>(Configuration);
            services.AddContentful(Configuration);
            services.AddSingleton<ContentfulApi, SdkContentfulApi>();
            services.AddSingleton<ContentfulHtmlRenderer>();

            // Setup Cache Busting
            services.AddSingleton<CacheBuster, ManualCacheBuster>()
                // These are registered manually, as more services are added that have a cache,
                // they should follow the patterns here.
                .AddTransient<BustCache>(
                    services => services.GetRequiredService<ContentfulApi>()
                ).AddTransient<BustCache>(
                    services => services.GetRequiredService<SitemapGenerator>()
                ).AddTransient<BustCache>(
                    services => services.GetRequiredService<PageMetadataGenerator>()
                );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDocumentMetadata();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();

                // Here we register <domain>/robots.txt to return the generated Sitemap
                endpoints.MapGet(
                    "/robots.txt",
                    async context =>
                    {
                        await context.Response.WriteAsync(
                            context.RequestServices
                                .GetRequiredService<RobotsTxtGenerator>()
                                .Generate()
                        );
                    }
                );


                // Here we register <domain>/sitemap.xml to return the generated Sitemap
                endpoints.MapGet(
                    "/sitemap.xml",
                    async context =>
                    {
                        await context.Response.WriteAsync(
                            await context.RequestServices
                                .GetRequiredService<SitemapGenerator>()
                                .Generate()
                        );
                    }
                );

                // Here we register <domain>/feed.xml to return the generated RSS Feed
                endpoints.MapGet(
                    "/feed.xml",
                    async context =>
                    {
                        await context.Response.WriteAsync(
                            await context.RequestServices
                                .GetRequiredService<RssFeedGenerator>()
                                .Generate()
                        );
                    }
                );

                // This is our cache busting endpoint that when called will trigger the CacheBuster service.
                // This can be registered in Contentful, so any changes in Contentful will trigger clear the cache.
                endpoints.MapPost(
                    "/webhook/cache-buster",
                    async context =>
                    {
                        await context.RequestServices
                            .GetRequiredService<CacheBuster>()
                            .BustCache();
                        await context.Response.WriteAsync("Ok");
                    }
                );

                endpoints.MapFallbackToPage("blog/{**slug}", "/_Host");
                endpoints.MapFallbackToPage("{*path:regex(^(?!api).*$)}", "/_Host");

                //endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}

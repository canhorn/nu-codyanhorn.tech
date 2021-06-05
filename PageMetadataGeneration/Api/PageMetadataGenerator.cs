namespace CodyAnhorn.Tech.PageMetadataGeneration.Api
{
    using System.Threading.Tasks;
    using CodyAnhorn.Tech.CacheBusting.Api;
    using CodyAnhorn.Tech.Localization;
    using EventHorizon.Blazor.DocumentMetadata.Api;
    using Microsoft.Extensions.Localization;

    public interface PageMetadataGenerator
        : BustCache
    {
        Task Generate(
           IDocumentMetadataCollection registrator,
           IStringLocalizer<LocalizationResource> localizer
       );
    }
}

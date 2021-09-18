namespace CodyAnhorn.Tech.ContentfulSdk.Sdk
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CodyAnhorn.Tech.ContentfulSdk.Api;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Uses;
    using Contentful.Core.Search;
    using Microsoft.Extensions.Logging;

    public partial class SdkContentfulApi
        : ContentfulApi
    {
        private static readonly ConcurrentBag<UsesEntry> UsesEntryCache = new();

        public async Task<IEnumerable<UsesEntry>> GetAllUsesEntries()
        {
            try
            {
                if (UsesEntryCache.Any())
                {
                    return UsesEntryCache.AsEnumerable();
                }

                var usesEntries = new List<UsesEntry>();
                var shouldQueryMore = true;
                var page = 1;

                while (shouldQueryMore)
                {
                    var (Total, Items) = await GetUsesEntries(
                        page
                    );
                    usesEntries.AddRange(
                        Items
                    );

                    shouldQueryMore = usesEntries.Count < Total;
                    page++;
                }

                foreach (var usesEntry in usesEntries)
                {
                    UsesEntryCache.Add(
                        usesEntry
                    );
                }

                return usesEntries;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to get Uses Entries"
                );
                return new List<UsesEntry>();
            }
        }

        private async Task<(int Total, IEnumerable<UsesEntry> Items)> GetUsesEntries(
            int page
        )
        {
            try
            {
                var queryLimit = 100;
                var skipMultiplier = page == 1 ? 0 : page - 1;
                var skip = skipMultiplier > 0
                    ? queryLimit * skipMultiplier
                    : 0;

                var queryBuilder = QueryBuilder<UsesEntry>.New
                    .ContentTypeIs(
                        "usesEntry"
                    ).Limit(
                        queryLimit
                    ).Skip(
                        skip
                    );

                // Check Cache
                // Not found in Cache, look up new based on query
                var result = await _client.GetEntries(
                    queryBuilder
                );

                return (result.Total, result.Items);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to get Content for 'UsesEntry'"
                );
                return (0, new List<UsesEntry>());
            }
        }
    }
}

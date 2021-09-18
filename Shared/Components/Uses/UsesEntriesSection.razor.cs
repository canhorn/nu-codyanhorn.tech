namespace CodyAnhorn.Tech.Shared.Components.Uses
{
    using System.Collections.Generic;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Uses;
    using Microsoft.AspNetCore.Components;

    public class UsesEntriesSectionBase
        : StandardComponentBase
    {
        [Parameter]
        public IEnumerable<UsesEntry> UsesEntries { get; set; } = new List<UsesEntry>();
    }
}

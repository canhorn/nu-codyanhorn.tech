namespace CodyAnhorn.Tech.ContentfulSdk.Model.Uses
{
    using System.Collections.Generic;
    using CodyAnhorn.Tech.ContentfulSdk.Model.Page;
    using Contentful.Core.Models;
    public class UsesEntry
    {
        public string Name { get; set; } = string.Empty;
        public PageBody? Description { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public HyperlinkEntity? WebsiteLink { get; set; }
        public Asset? Image { get; set; }
    }
}

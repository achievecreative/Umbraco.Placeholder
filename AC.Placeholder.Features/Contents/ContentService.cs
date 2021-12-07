using System;
using AC.Placeholder.Extensions;
using AC.Placeholder.Features.Panels;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace AC.Placeholder.Features.Contents
{
    public class ContentService : IContentService
    {
        public int Order => Int32.MinValue;
        public Content Get(IPublishedContent content)
        {
            if (!content.IsComponent())
            {
                return null;
            }

            return new Content()
            {
                RawContent = content.GetValue<string>("content")
            };
        }
    }
}

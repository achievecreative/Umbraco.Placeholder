using System;
using AC.Placeholder.Extensions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace AC.Placeholder.Features.Panels
{
    public class FeaturePanelService : IFeaturePanelService
    {
        public int Order => Int32.MinValue;
        public FeaturePanel Get(IPublishedContent content)
        {
            if (!content.IsComponent())
            {
                return null;
            }

            var image = content.GetValue<IPublishedContent>("image");
            var imageUrl = image?.Url();
            var imageColSize = content.GetValue<int>("imageColumns");
            var contentColSize = string.IsNullOrEmpty(imageUrl) ? 12 : 12 - imageColSize;
            if (contentColSize < 1)
            {
                contentColSize = 12;
            }

            return new FeaturePanel()
            {
                HasImage = !string.IsNullOrEmpty(imageUrl),
                ImageUrl = imageUrl,
                ImageAlt = image.GetStringValue(content.Name, "umbracoAlt"),
                Content = content.GetValue<string>("content"),
                ImageAtTheRight = content.GetValue<bool>("imageAtTheRight"),
                ImageColumnSize = imageColSize,
                ContentColumnSize = contentColSize
            };
        }
    }
}

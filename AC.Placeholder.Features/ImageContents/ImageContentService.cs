using System;
using AC.Placeholder.Extensions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace AC.Placeholder.Features.ImageContents
{
    public class ImageContentService : IImageContentService
    {
        public int Order => Int32.MinValue;
        public ImageContent Get(IPublishedContent content)
        {
            if (!content.IsComponent())
            {
                return null;
            }

            var image = content.GetValue<IPublishedContent>("image");
            var imageUrl = image.Url();
            var imageColSize = content.GetValue<int>("imageColumnSize");
            var contentColSize = string.IsNullOrEmpty(imageUrl) ? 12 : 12 - imageColSize;
            if (contentColSize < 1)
            {
                contentColSize = 12;
            }

            return new ImageContent()
            {
                HasImage = !string.IsNullOrEmpty(imageUrl),
                ImageUrl = imageUrl,
                ImageAlt = image.GetStringValue(content.Name, "umbracoAlt"),
                Content = content.GetValue<string>("content"),
                ImageAtTheRight = content.GetValue<bool>("imageAtRight"),
                ImageColumnSize = imageColSize,
                ContentColumnSize = contentColSize
            };
        }
    }
}

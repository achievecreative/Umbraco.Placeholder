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

            return new ImageContent()
            {
                ImageUrl = image?.Url(),
                ImageAlt = image.GetStringValue(content.Name, "umbracoAlt"),
                Content = content.GetValue<string>("content"),
                ImageAtTheRight = content.GetValue<bool>("imageAtRight"),
                ImageColumnSize = content.GetValue<int>("imageColumnSize")
            };
        }
    }
}

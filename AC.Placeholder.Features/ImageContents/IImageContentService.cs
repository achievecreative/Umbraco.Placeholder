using Umbraco.Cms.Core.Models.PublishedContent;

namespace AC.Placeholder.Features.ImageContents
{
    public interface IImageContentService : IReplaceable
    {
        ImageContent Get(IPublishedContent content);
    }
}

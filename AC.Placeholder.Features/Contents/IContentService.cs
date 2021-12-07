using Umbraco.Cms.Core.Models.PublishedContent;

namespace AC.Placeholder.Features.Contents
{
    public interface IContentService:IReplaceable
    {
        Content Get(IPublishedContent content);
    }
}

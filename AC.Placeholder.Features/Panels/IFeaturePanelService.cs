using Umbraco.Cms.Core.Models.PublishedContent;

namespace AC.Placeholder.Features.Panels
{
    public interface IFeaturePanelService : IReplaceable
    {
        FeaturePanel Get(IPublishedContent content);
    }
}

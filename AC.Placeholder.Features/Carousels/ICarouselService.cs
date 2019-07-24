using Umbraco.Core.Models.PublishedContent;

namespace AC.Placeholder.Features.Carousels
{
    public interface ICarouselService : IReplaceable
    {
        Carousel Get(IPublishedContent content);
    }
}
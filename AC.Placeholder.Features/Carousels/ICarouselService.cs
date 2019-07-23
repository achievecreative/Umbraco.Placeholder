using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models.PublishedContent;

namespace AC.Placeholder.Features.Features.Carousels
{
    public interface ICarouselService
    {
        Carousel Get(IPublishedContent content);
    }
}
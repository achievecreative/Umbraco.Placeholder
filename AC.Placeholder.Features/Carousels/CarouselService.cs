using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models.PublishedContent;

namespace AC.Placeholder.Features.Carousels
{
    class CarouselService : ICarouselService
    {
        public Carousel Get(IPublishedContent content)
        {
            throw new NotImplementedException();
        }

        public int Order => Int32.MinValue;
    }
}
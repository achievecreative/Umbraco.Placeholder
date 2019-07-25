using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AC.Placeholder.Extensions;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace AC.Placeholder.Features.Carousels
{
    class CarouselService : ICarouselService
    {
        public Carousel Get(IPublishedContent content)
        {
            if (!content.IsComponent())
            {
                return null;
            }

            return new Carousel()
            {
                HasControl = content.GetValue<bool>("control"), HasIndicator = content.GetValue<bool>("indicator"),
                Items = content.Children.Select(x=>new CarouselItem()
                {
                    Image = x.GetMediaUrl("image"),
                    CallToAction = x.GetValue<Link>("callToAction"),
                    Content = x.GetValue<string>("content"),
                    ContentPosition = x.GetValue<string>("contentPosition"),
                    Title = x.GetValue<string>("title")
                })
            };
        }

        public int Order => Int32.MinValue;
    }
}
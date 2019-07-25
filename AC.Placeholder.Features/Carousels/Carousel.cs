using System.Collections.Generic;

namespace AC.Placeholder.Features.Carousels
{
    public class Carousel
    {
        public bool HasIndicator { get; set; }

        public bool HasControl { get; set; }

        public IEnumerable<CarouselItem> Items { get; set; }
    }
}
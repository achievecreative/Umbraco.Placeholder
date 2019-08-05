using System.Collections.Generic;
using AC.Placeholder.Features.Models;

namespace AC.Placeholder.Features.Carousels
{
    public class Carousel : Component
    {
        public bool HasIndicator { get; set; }

        public bool HasControl { get; set; }

        public IEnumerable<CarouselItem> Items { get; set; }
    }
}
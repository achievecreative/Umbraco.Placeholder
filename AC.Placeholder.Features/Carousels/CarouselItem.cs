using Umbraco.Cms.Core.Models;

namespace AC.Placeholder.Features.Carousels
{
    public class CarouselItem
    {
        public string Image { get; set; }

        public string Content { get; set; }

        public string ContentPosition { get; set; }

        public Link CallToAction { get; set; }

        public string Title { get; set; }
    }
}
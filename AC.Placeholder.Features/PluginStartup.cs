using AC.Placeholder.Features.Carousels;
using AC.Placeholder.Features.ImageContents;
using AC.Placeholder.Features.Navigations;
using AC.Placeholder.Features.Panels;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace AC.Placeholder.Features
{
    public class PluginStartup : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddScoped<INavigationService, NavigationService>();
            builder.Services.AddScoped<ICarouselService, CarouselService>();
            builder.Services.AddScoped<IPanelListService, PanelListService>();
            builder.Services.AddScoped<IImageContentService, ImageContentService>();
        }
    }
}
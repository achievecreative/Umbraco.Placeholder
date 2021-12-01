using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Placeholder.Features.Carousels;
using AC.Placeholder.Features.Navigations;
using AC.Placeholder.Features.Panels;

namespace AC.Placeholder.Features
{
    public class FeatureServices
    {
        static FeatureServices()
        {
            Instance = new FeatureServices();
        }

        public INavigationService Navigation => Get<INavigationService>();

        public ICarouselService Carousel => Get<ICarouselService>();

        public IPanelListService PanelList => Get<IPanelListService>();

        public T Get<T>() where T : IReplaceable
        {
            return DependencyResolver.Current.GetServices<T>().OrderBy(x => x.Order).LastOrDefault();
        }

        public static FeatureServices Instance { get; private set; }
    }
}
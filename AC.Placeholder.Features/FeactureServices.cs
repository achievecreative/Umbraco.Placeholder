using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Placeholder.Features.Navigations;

namespace AC.Placeholder.Features
{
    public class FeactureServices
    {
        static FeactureServices()
        {
            Instance = new FeactureServices();
        }

        public INavigationService Navigation
        {
            get
            {
                return DependencyResolver.Current.GetServices<INavigationService>().OrderBy(x => x.Order).LastOrDefault();
            }
        }

        public static FeactureServices Instance { get; private set; }
    }
}
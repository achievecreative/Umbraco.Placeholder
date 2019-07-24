using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AC.Placeholder.Extensions;
using AC.Placeholder.Features.Features.Models;
using Umbraco.Core.Composing;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace AC.Placeholder.Features.Navigations
{
    internal class NavigationService : INavigationService
    {
        public int Order => int.MinValue;

        public IEnumerable<NavigationItem> GetNavigation(IPublishedContent current)
        {
            return GetNavigation(current, 1);
        }

        public IEnumerable<NavigationItem> GetNavigation(IPublishedContent current, int maxLevel)
        {
            if (current == null)
            {
                return null;
            }

            //Home item
            var root = current.AncestorOrSelf(1);

            var navs = new List<NavigationItem>()
            {
                new NavigationItem()
                {
                    Title = GetNavigationTitle(root),
                    Activate =  current.Id == root.Id,
                    Url = "/"
                }
            };

            navs.AddRange(root.Children.Where(x => x.IsPage()).Select(x =>
            {
                var hide = x.GetValue<bool>("hideInMainNavigation");
                if (hide)
                {
                    return null;
                }

                return new NavigationItem()
                {
                    Activate = current.Id == x.Id,
                    Url = x.Url(),
                    Title = GetNavigationTitle(x)
                };
            }));

            return navs;
        }

        private string GetNavigationTitle(IPublishedContent content)
        {
            var title = content.GetValue<string>("navigationTitle");
            if (string.IsNullOrEmpty(title))
            {
                title = content.Name;
            }

            return title;
        }
    }
}
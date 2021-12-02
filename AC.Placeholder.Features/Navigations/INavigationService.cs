using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace AC.Placeholder.Features.Navigations
{
    public interface INavigationService: IReplaceable
    {
        IEnumerable<NavigationItem> GetNavigation(IPublishedContent current);

        IEnumerable<NavigationItem> GetNavigation(IPublishedContent current, int maxLevel);
    }
}
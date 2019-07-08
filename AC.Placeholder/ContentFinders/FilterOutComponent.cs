using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AC.Placeholder.Extensions;
using Umbraco.Web.Routing;

namespace AC.Placeholder.ContentFinders
{
    /// <summary>
    /// Filter out all components from the request, so it's not accessable via URL
    /// </summary>
    public class FilterOutComponent : IContentFinder
    {
        public bool TryFindContent(PublishedRequest frequest)
        {
            if (frequest.PublishedContent.IsComponent())
            {
                frequest.SetRedirectPermanent("~/");
                return true;
            }

            return false;
        }
    }
}
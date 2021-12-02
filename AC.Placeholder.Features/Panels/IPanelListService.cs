using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace AC.Placeholder.Features.Panels
{
    public interface IPanelListService : IReplaceable
    {
        PanelList Get(IPublishedContent content);
    }
}
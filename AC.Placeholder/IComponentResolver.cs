using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace AC.Placeholder
{
    public interface IComponentResolver: IReplaceable
    {
        IEnumerable<IPublishedContent> Find(IPublishedContent page, string key);
    }
}

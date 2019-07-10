using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;

namespace AC.Placeholder
{
    public interface IComponentResolver
    {
        IEnumerable<IPublishedContent> Find(IPublishedContent page, string key);

        int Order { get; }
    }
}

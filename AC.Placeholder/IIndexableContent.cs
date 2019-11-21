using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AC.Placeholder
{
    public interface IIndexableContent
    {
        IDictionary<string, string> IndexableContents { get; }
    }
}
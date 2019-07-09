using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AC.Placeholder.Components;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Web;

namespace AC.Placeholder
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class FilterOutComponentFinderComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<FilterOutComponent>();
        }
    }
}
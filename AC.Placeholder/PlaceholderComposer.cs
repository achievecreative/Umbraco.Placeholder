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
    public class PlaceholderComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            //Redirect Component to home page or it's parent page
            composition.Components().Append<FilterOutComponent>();

            composition.RegisterFor<IComponentResolver, ComponmentResolver>(Lifetime.Singleton);
        }
    }
}
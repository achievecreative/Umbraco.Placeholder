using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Placeholder.Components;
using AC.Placeholder.ContentStructures;
using AC.Placeholder.Resolvers;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Web;

namespace AC.Placeholder
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class ServicesRegister : IUserComposer
    {
        public void Compose(Composition composition)
        {
            //Redirect Component to home page or it's parent page
            composition.Components().Append<FilterOutComponent>();

            //Setup content structure
            composition.Components().Append<InitialComponentFolderComponent>();

            // Resolvers
            composition.Register(typeof(IComponentResolver), typeof(ComponentResolver), Lifetime.Singleton);
            composition.Register(typeof(IPlaceholderResolver), typeof(PlaceholderResolver), Lifetime.Singleton);
        }
    }
}
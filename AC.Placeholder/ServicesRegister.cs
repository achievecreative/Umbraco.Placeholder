using System;
using System.Linq;
using AC.Placeholder.Components;
using AC.Placeholder.ContentStructures;
using AC.Placeholder.Extensions;
using AC.Placeholder.Resolvers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace AC.Placeholder
{
    public class ServicesRegister : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            //Redirect Component to home page or it's parent page
            builder.Components().Append<FilterOutComponent>();

            //Setup content structure
            builder.Components().Append<InitialComponentFolderComponent>();

            //resolvers
            builder.Services.TryAddSingleton<IComponentResolver, ComponentResolver>();
            builder.Services.TryAddSingleton<IPlaceholderResolver, PlaceholderResolver>();
        }
    }
}
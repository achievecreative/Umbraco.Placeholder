using AC.Placeholder.Notifications;
using AC.Placeholder.Resolvers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace AC.Placeholder
{
    public class PluginStartup : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            //application start, set ServiceProvider
            builder.AddNotificationHandler<UmbracoApplicationStartingNotification, ApplicationStartingNotificationHandler>();

            //Setup content structure
            builder.AddNotificationHandler<ContentSavedNotification, ContentSaveNotificationHandler>();

            //resolvers
            builder.Services.TryAddSingleton<IComponentResolver, ComponentResolver>();
            builder.Services.TryAddSingleton<IPlaceholderResolver, PlaceholderResolver>();

            //Redirect Component to home page or it's parent page
            builder.AddNotificationHandler<RoutingRequestNotification, RoutingRequestNotificationHandler>();
        }
    }
}
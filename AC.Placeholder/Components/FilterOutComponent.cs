using System;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using AC.Placeholder.Notifications;
using Umbraco.Cms.Core.Notifications;

namespace AC.Placeholder.Components
{
    /// <summary>
    /// Filter out all components from the request, so it's not accessable via URL
    /// </summary>
    public class FilterOutComponent : IComponent
    {
        private IUmbracoBuilder _UmbracoBuilder;
        public FilterOutComponent(IUmbracoBuilder umbracoBuilder)
        {
            _UmbracoBuilder = umbracoBuilder;
        }

        public void Initialize()
        {
            _UmbracoBuilder.AddNotificationHandler<RoutingRequestNotification, RoutingRequestNotificationHandler>();
        }

        public void Terminate()
        {

        }
    }
}
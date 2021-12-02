using System;
using AC.Placeholder.Extensions;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace AC.Placeholder.Notifications
{
    public class ApplicationStartingNotificationHandler : INotificationHandler<UmbracoApplicationStartingNotification>
    {
        private readonly IServiceProvider _serviceProvider;

        public ApplicationStartingNotificationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Handle(UmbracoApplicationStartingNotification notification)
        {
            InternalServiceProvider.Instance = _serviceProvider;
        }
    }
}

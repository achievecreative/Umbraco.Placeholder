using AC.Placeholder.Extensions;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace AC.Placeholder.Notifications
{
    public class RoutingRequestNotificationHandler : INotificationHandler<RoutingRequestNotification>
    {
        public void Handle(RoutingRequestNotification notification)
        {
            var publishedContent = notification.RequestBuilder.PublishedContent;
            if (!publishedContent.TemplateId.HasValue && publishedContent.Parent.IsComponent())
            {
                notification.RequestBuilder.SetRedirect("~/");
            }

            if (publishedContent.IsComponent())
            {
                notification.RequestBuilder.SetRedirect("~/");
            }
        }
    }
}

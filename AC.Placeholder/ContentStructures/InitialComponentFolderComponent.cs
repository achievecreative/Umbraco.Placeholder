using AC.Placeholder.Notifications;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace AC.Placeholder.ContentStructures
{
    public class InitialComponentFolderComponent : IComponent
    {
        private IUmbracoBuilder umbracoBuilder;
        public InitialComponentFolderComponent(IUmbracoBuilder umbracoBuilder)
        {
            this.umbracoBuilder = umbracoBuilder;
        }

        public void Initialize()
        {
            this.umbracoBuilder.AddNotificationHandler<ContentSavedNotification, ContentSaveNotificationHandler>();
        }

        public void Terminate()
        {
            
        }
    }
}
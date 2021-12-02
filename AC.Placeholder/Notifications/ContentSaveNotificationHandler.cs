using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AC.Placeholder.Extensions;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.Entities;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;

namespace AC.Placeholder.Notifications
{
    public class ContentSaveNotificationHandler : INotificationHandler<ContentSavedNotification>
    {
        private IContentService contentService;
        public ContentSaveNotificationHandler(IContentService contentService)
        {
            this.contentService = contentService;
        }

        public void Handle(ContentSavedNotification notification)
        {
            var entity = notification.SavedEntities.FirstOrDefault();
            var isNew = ((IRememberBeingDirty)entity)?.WasPropertyDirty("Id") ?? false;
            if (isNew && entity.IsPage())
            {
                //new component folder for new node
                var content = contentService.Create(Constants.ComponentFolderName, entity.Id, Constants.ComponentFolderAlias);
                contentService.Save(content);
            }
        }
    }
}

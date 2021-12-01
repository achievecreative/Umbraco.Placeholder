using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AC.Placeholder.Extensions;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Models.Entities;
using Umbraco.Cms.Core.Services.Implement;

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
            this.umbracoBuilder.AddNotificationHandler<>()
            ContentService.Saved += ContentService_Saved;
        }

        private void ContentService_Saved(Umbraco.Core.Services.IContentService sender, Umbraco.Core.Events.ContentSavedEventArgs e)
        {
            var entity = e.SavedEntities.FirstOrDefault();
            var isNew = ((IRememberBeingDirty) entity)?.WasPropertyDirty("Id") ?? false;
            if (isNew && entity.IsPage())
            {
                //new component folder for new node
                var content = sender.Create(Constants.ComponentFolderName, entity.Id, Constants.ComponentFolderAlias);
                sender.Save(content, raiseEvents: false);
            }
        }

        public void Terminate()
        {
            ContentService.Saved -= ContentService_Saved;
        }
    }
}
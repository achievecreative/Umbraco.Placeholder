using System;
using System.Linq;
using Umbraco.Core.Composing;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace AC.Placeholder.Extensions
{
    public static class ContentExtension
    {
        public static bool IsComponent(this IContent content)
        {
            if (content == null)
            {
                return false;
            }

            var contentType = Current.Services.ContentTypeService.Get(content.ContentTypeId);
            if (contentType == null)
            {
                return false;
            }

            return contentType.Alias == Constants.PlaceholderSelectorEditorAlias ||
                   (contentType.ContentTypeComposition?.Any(x => x.Alias == Constants.ComponentBaseDocumentAlias) ?? false);
        }

        public static bool IsComponent(this IPublishedContent content)
        {
            if (content == null)
            {
                return false;
            }

            return content.ContentType.Alias == Constants.ComponentFolderName ||
                   (content.ContentType.CompositionAliases?.Any(x => x == Constants.ComponentBaseDocumentAlias) ?? false);
        }
    }
}
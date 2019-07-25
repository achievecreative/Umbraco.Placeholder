using System;
using System.Linq;
using Umbraco.Core;
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

            return content.IsComponentFolder() ||
                   (contentType.ContentTypeComposition?.Any(x => x.Alias == Constants.ComponentBaseDocumentAlias) ?? false);
        }

        public static bool IsComponent(this IPublishedContent content)
        {
            if (content == null)
            {
                return false;
            }

            return content.IsComponentFolder() ||
                   (content.ContentType.CompositionAliases?.Any(x => x == Constants.ComponentBaseDocumentAlias) ?? false);
        }

        public static bool IsComponentFolder(this IPublishedContent content)
        {
            return content != null && content.ContentType.Alias == Constants.ComponentFolderAlias;
        }

        public static bool IsComponentFolder(this IContent content)
        {
            return content != null && content.ContentType.Alias == Constants.ComponentFolderAlias;
        }

        public static bool IsPage(this IContent content)
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

            return contentType.ContentTypeCompositionExists(Constants.PageBaesAlias);
        }

        public static bool IsPage(this IPublishedContent content)
        {
            if (content == null)
            {
                return false;
            }

            return content.ContentType.CompositionAliases.Any(x => x == Constants.PageBaesAlias);
        }

        public static T GetValue<T>(this IPublishedContent content, string alias)
        {
            if (content == null || string.IsNullOrEmpty(alias))
            {
                return default(T);
            }

            var property = content.GetProperty(alias);
            if (property == null)
            {
                return default(T);
            }

            return property.GetValue().TryConvertTo<T>().Result;
        }

        public static string GetMediaUrl(this IPublishedContent content, string alias)
        {
            if (content == null || string.IsNullOrEmpty(alias))
            {
                return null;
            }

            var media = content.GetValue<IPublishedContent>(alias);
            if (media == null)
            {
                return null;
            }

            return media.GetUrl();
        }
    }
}
using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.UmbracoContext;
using Umbraco.Extensions;


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
            
            ServiceContext.CreatePartialServiceContext(content);

            var contentType = Current.Services.ContentTypeService.Get(content.ContentTypeId);
            if (contentType == null)
            {
                return false;
            }

            content.

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

            return contentType.ContentTypeCompositionExists(Constants.PageBaseAlias);
        }

        public static bool IsPage(this IPublishedContent content)
        {
            if (content == null)
            {
                return false;
            }

            return content.ContentType.CompositionAliases.Any(x => x == Constants.PageBaseAlias);
        }

        public static IPublishedContent SiteFolder(this IPublishedContent content)
        {
            return content?.AncestorOrSelf(Constants.SiteFolderAlias);
        }

        public static IContent SiteFolder(this IContent content)
        {
            if (content == null)
            {
                return null;
            }

            return Current.Services.ContentService.GetAncestors(content).FirstOrDefault(x => x.ContentType?.Alias == Constants.SiteFolderAlias);
        }

        public static IPublishedContent SiteSettings(this IPublishedContent content)
        {
            return content.SiteFolder()?.Children.FirstOrDefault(x => x.ContentType.Alias == Constants.SiteSettingsAlias);
        }

        public static IContent SiteSettings(this IContent content)
        {
            var siteFolder = content.SiteFolder();
            if (siteFolder == null)
            {
                return null;
            }

            var filter = Current.SqlContext.Query<IContent>().Where(x => x.ContentType.Alias == Constants.SiteSettingsAlias);
            return Current.Services.ContentService.GetPagedChildren(siteFolder.Id, 1, 1, out long total, filter).FirstOrDefault();
        }

        /// <summary>
        /// Return the home node of current node
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static IPublishedContent StartNode(this IPublishedContent content)
        {
            return content.SiteFolder()?.Children(x => x.ContentType.Alias == Constants.HomeAlias).FirstOrDefault();
        }

        /// <summary>
        /// Return the home node of current node
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static IContent StartNode(this IContent content)
        {
            var siteFolder = content.SiteFolder();
            if (siteFolder == null)
            {
                return null;
            }

            var filter = Current.SqlContext.Query<IContent>().Where(x => x.ContentType.Alias == Constants.HomeAlias);
            return Current.Services.ContentService.GetPagedChildren(siteFolder.Id, 1, 1, out long total, filter).FirstOrDefault();
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
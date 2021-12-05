using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.DependencyInjection;
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

            var contentType = InternalServiceProvider.Instance?.GetService<IContentTypeService>()?.Get(content.ContentTypeId);
            if (contentType == null)
            {
                return false;
            }

            return content.IsComponentFolder() || (contentType.ContentTypeComposition?.Any(x => x.Alias == Constants.ComponentBaseDocumentAlias) ?? false);
        }

        /// <summary>
        /// Check is the content a AC.Placeholder component or not.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
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

            var contentType = InternalServiceProvider.Instance?.GetService<IContentTypeService>()?.Get(content.ContentTypeId);
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

            return InternalServiceProvider.Instance?.GetService<IContentService>()?.GetAncestors(content).FirstOrDefault(x => x.ContentType?.Alias == Constants.SiteFolderAlias);
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

            var filter = InternalServiceProvider.Instance?.GetService<ISqlContext>()?.Query<IContent>().Where(x => x.ContentType.Alias == Constants.SiteSettingsAlias);
            return InternalServiceProvider.Instance?.GetService<IContentService>()?.GetPagedChildren(siteFolder.Id, 1, 1, out long total, filter).FirstOrDefault();
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

            var filter = InternalServiceProvider.Instance?.GetService<ISqlContext>()?.Query<IContent>().Where(x => x.ContentType.Alias == Constants.HomeAlias);
            return InternalServiceProvider.Instance?.GetService<IContentService>()?.GetPagedChildren(siteFolder.Id, 1, 1, out long total, filter).FirstOrDefault();
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

        /// <summary>
        /// Try to get the string value for given properties. You can pass in a list of properties and a default value
        /// <para>
        /// 
        /// </para>
        /// </summary>
        /// <param name="content"></param>
        /// <param name="defaultValue"></param>
        /// <param name="propertyAliases"></param>
        /// <returns></returns>
        public static string GetStringValue(this IPublishedContent content, string defaultValue, params string[] propertyAliases)
        {
            if (content == null || propertyAliases == null || propertyAliases.Length == 0)
            {
                return defaultValue;
            }

            foreach (var propertyAlias in propertyAliases)
            {
                var property = content.GetProperty(propertyAlias);
                if (property == null)
                {
                    continue;
                }

                var value = property.GetValue().TryConvertTo<string>().Result;
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
            }

            return defaultValue;
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

            return media.Url();
        }
    }
}
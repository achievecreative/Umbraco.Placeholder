﻿
using AC.Placeholder.Extensions;
using AC.Placeholder.Features.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace AC.Placeholder.Features
{
    public static class ContentExtensions
    {

        /// <summary>
        /// Return null if the current node not a page
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static PageSetting Page(this IPublishedContent source)
        {
            if (!source.IsPage())
            {
                return null;
            }

            return new PageSetting()
            {
                PageDescription = source.GetValue<string>("pageDescription"),
                PageKeywords = source.GetValue<string>("pageKeywords"),
                ExcludedFromSearch = source.GetValue<bool>("excludedFromSearch"),
                HideInMainNavigation = source.GetValue<bool>("hideInMainNavigation")
            };
        }

        /// <summary>
        /// Return null if the current node not a component
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static StyleSetting Styles(this IPublishedContent source)
        {
            if (!source.IsComponent())
            {
                return null;
            }

            var fullWidth = source.GetValue<bool>("fullWidth");
            var cssClass = $"{source.ContentType.Alias.ToLower()} {source.GetValue<string>("componmentCssClass")} {(fullWidth ? "container-fluid" : "container")}";

            return new StyleSetting()
            {
                CssClass = cssClass,
                FullWidth = fullWidth
            };
        }
    }
}
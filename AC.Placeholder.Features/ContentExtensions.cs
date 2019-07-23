using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AC.Placeholder.Extensions;
using AC.Placeholder.Features.Foundations.Models;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;

namespace AC.Placeholder.Features
{
    public static class ContentExtensions
    {
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

        public static Page Page(this IPublishedContent source)
        {
            if (!source.IsPage())
            {
                return null;
            }

            return new Page()
            {
                PageDescription = source.GetValue<string>("pageDescription"),
                PageKeywords = source.GetValue<string>("pageKeywords"),
                ExcludedFromSearch = source.GetValue<bool>("excludedFromSearch"),
                HideInMainNavigation = source.GetValue<bool>("hideInMainNavigation")
            };
        }

        public static Styles Styles(this IPublishedContent source)
        {
            if (!source.IsComponent())
            {
                return null;
            }

            var cssClass = source.GetValue<string>("cssClass");

            var fullWidth = source.GetValue<bool>("fullWidth");
            cssClass += " " + (fullWidth ? "container-fluid" : "container");

            return new Styles()
            {
                CssClass = cssClass,
                FullWidth = fullWidth
            };
        }
    }
}
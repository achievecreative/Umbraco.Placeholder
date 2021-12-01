﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LightInject;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace AC.Placeholder.Extensions
{
    public static class UmbracoHelperExtension
    {
        public static IHtmlString Placeholder(this UmbracoHelper helper, string key)
        {
            var resolver = DependencyResolver.Current.GetServices<IComponentResolver>()
                .LastOrDefault();

            if (resolver == null)
            {
                Current.Logger.Error(typeof(IComponentResolver), "Unable to found the component resolver");

                return new HtmlString("");
            }

            var pageContent = helper.AssignedContentItem;

            var components = resolver.Find(pageContent, key);

            var results = components.Select(x => RenderTemplate(helper, x.Id, x.TemplateId));

            return new HtmlString(string.Join("", results));
        }

        private static IHtmlString RenderTemplate(UmbracoHelper helper, int contentId, int? templateId)
        {
            try
            {
                return helper.RenderTemplate(contentId, templateId);
            }
            catch (Exception e)
            {
                if (HttpContext.Current.IsCustomErrorEnabled)
                {
                    return new HtmlString(e.Message);
                }
            }

            return new HtmlString(string.Empty);
        }
    }
}
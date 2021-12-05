using System;
using System.Linq;
using System.Threading.Tasks;
using J2N.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Web.Common;

namespace AC.Placeholder.Extensions
{
    public static class UmbracoHelperExtension
    {
        public static async Task<IHtmlEncodedString> PlaceholderAsync(this UmbracoHelper helper, string key)
        {
            var resolver = InternalServiceProvider.Instance.GetServices<IComponentResolver>().LastOrDefault();

            if (resolver == null)
            {
                InternalServiceProvider.Instance.GetService<ILogger<IComponentResolver>>().LogError("Unable to found the component resolver");

                return null;
            }

            var pageContent = helper.AssignedContentItem;

            var components = resolver.Find(pageContent, key);

            var list = new List<IHtmlEncodedString>();

            foreach (var component in components)
            {
                var result = await RenderTemplate(helper, component.Id, component.TemplateId);
                list.Add(result);
            }

            return new HtmlEncodedString(string.Join("", list));
        }

        private static async Task<IHtmlEncodedString> RenderTemplate(UmbracoHelper helper, int contentId, int? templateId)
        {
            try
            {
                return await helper.RenderTemplateAsync(contentId, templateId);
            }
            catch (Exception e)
            {
                var hostEnvironment = InternalServiceProvider.Instance.GetService<IHostEnvironment>();
                if (hostEnvironment.IsDevelopment())
                {
                    return new HtmlEncodedString(e.Message);
                }
            }

            return new HtmlEncodedString(string.Empty);
        }
    }
}
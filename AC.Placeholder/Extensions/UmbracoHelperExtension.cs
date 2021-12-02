using System;
using System.Linq;
using System.Threading.Tasks;
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
        public static IHtmlEncodedString Placeholder(this UmbracoHelper helper, string key)
        {

            var resolver = InternalServiceProvider.Instance.GetServices<IComponentResolver>().LastOrDefault();

            if (resolver == null)
            {
                InternalServiceProvider.Instance.GetService<ILogger<IComponentResolver>>().LogError("Unable to found the component resolver");

                return new HtmlEncodedString(string.Empty);
            }

            var pageContent = helper.AssignedContentItem;

            var components = resolver.Find(pageContent, key);

            var results = components.Select(x => RenderTemplate(helper, x.Id, x.TemplateId));

            return new HtmlEncodedString(string.Join("", results));
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
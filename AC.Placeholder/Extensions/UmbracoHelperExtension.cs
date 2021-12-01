using System;
using System.Linq;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Web.Common;

namespace AC.Placeholder.Extensions
{
    public static class UmbracoHelperExtension
    {
        public static IHtmlEncodedString Placeholder(this UmbracoHelper helper, string key)
        {

            var resolver = DependencyResolver.Current.GetServices<IComponentResolver>()
                .LastOrDefault();

            if (resolver == null)
            {
                Current.Logger.Error(typeof(IComponentResolver), "Unable to found the component resolver");

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
                helper.
                if (HttpContext.Current.IsCustomErrorEnabled)
                {
                    return new HtmlString(e.Message);
                }
            }

            return new HtmlEncodedString(string.Empty);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace AC.Placeholder.Extensions
{
    public static class UmbracoHelperExtension
    {
        public static IHtmlString Placeholder(this UmbracoHelper helper, string key)
        {
            var pageContent = helper.AssignedContentItem;

            var components = new List<IPublishedContent>();

            foreach (var content in pageContent.Children.Where(x=>x.ContentType.CompositionAliases.Contains(Constants.ComponentBaseDocumentAlias)))
            {
                var phProperty = content.Properties.FirstOrDefault(x => x.PropertyType.EditorAlias == Constants.PlaceholderSelectorEditorAlias);
                if (phProperty == null)
                {
                    continue;
                }

                var placeholder = phProperty.GetValue()?.ToString();
                if (!string.IsNullOrEmpty(placeholder) && placeholder == key)
                {
                    components.Add(content);
                }
            }

            var results = components.Select(x => helper.RenderTemplate(x.Id, x.TemplateId).ToHtmlString());

            return new HtmlString(string.Join("", results));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models.PublishedContent;

namespace AC.Placeholder.Components
{
    public class ComponmentResolver : IComponentResolver
    {
        public IEnumerable<IPublishedContent> Find(IPublishedContent page, string key)
        {
            var list = new List<IPublishedContent>();

            foreach (var content in page.Children.Where(x => x.ContentType.CompositionAliases.Contains(Constants.ComponentBaseDocumentAlias)))
            {
                var phProperty = content.Properties.FirstOrDefault(x => x.PropertyType.EditorAlias == Constants.PlaceholderSelectorEditorAlias);
                if (phProperty == null)
                {
                    continue;
                }

                var placeholder = phProperty.GetValue()?.ToString();
                if (!string.IsNullOrEmpty(placeholder) && placeholder == key)
                {
                    list.Add(content);
                }
            }

            return list;
        }

        public int Order { get { return Int32.MinValue; } }
    }
}
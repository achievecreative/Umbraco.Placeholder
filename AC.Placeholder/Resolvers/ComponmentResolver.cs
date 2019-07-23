using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;

namespace AC.Placeholder.Resolvers
{
    public class ComponmentResolver : IComponentResolver
    {
        public IEnumerable<IPublishedContent> Find(IPublishedContent page, string key)
        {
            var list = new List<IPublishedContent>();

            var componentFolder = page.Children.FirstOrDefault(x=>x.ContentType.Alias == Constants.ComponentFolderAlias);
            if (componentFolder == null)
            {
                return list;
            }

            foreach (var content in componentFolder.Children.Where(x => x.ContentType.CompositionAliases.Contains(Constants.ComponentBaseDocumentAlias)))
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

        public int Order => Int32.MinValue;
    }
}
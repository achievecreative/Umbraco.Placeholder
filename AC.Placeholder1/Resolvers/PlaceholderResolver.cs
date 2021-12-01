using System;
using System.Linq;
using System.Text.RegularExpressions;
using AC.Placeholder.Models;
using Umbraco.Core.Composing;

namespace AC.Placeholder.Resolvers
{
    public class PlaceholderResolver : IPlaceholderResolver
    {
        static readonly Regex Regex = new Regex("@Umbraco\\.Placeholder\\(\"(\\w+)\"\\)");

        public PlaceholderModel Find(int pageContentId)
        {
            var page = Current.Services.ContentService.GetById(pageContentId);

            if (!page.TemplateId.HasValue)
            {
                return null;
            }

            var template = Current.Services.ContentTypeService.Get(page.ContentTypeId).AllowedTemplates.FirstOrDefault(x => x.Id == page.TemplateId.Value);
            if (template == null)
            {
                return null;
            }

            var matches = Regex.Matches(template.Content);
            var model = new PlaceholderModel()
            {
                PageId = page.Id,
                TemplateId = template.Id,
                Placholders = matches.Cast<Match>().Select(x => x.Groups.Cast<Group>().Select(g => g.Value).LastOrDefault()).ToArray()
            };

            return model;
        }

        public int Order => Int32.MinValue;
    }
}
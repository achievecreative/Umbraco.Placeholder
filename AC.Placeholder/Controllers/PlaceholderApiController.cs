using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using AC.Placeholder.Models;
using HtmlAgilityPack;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;


namespace AC.Placeholder.Controllers
{
    [PluginController("AC")]
    public class PlaceholderApiController : UmbracoAuthorizedJsonController
    {
        static Regex regex = new Regex("@Umbraco\\.Placeholder\\(\"(\\w+)\"\\)");

        [HttpGet]
        public JsonResult<PlaceholderModel> FindPlaceholder(int? nodeId)
        {
            if ((nodeId ?? 0) < 1)
            {
                return Json(new PlaceholderModel());
            }

            var currentNode = Services.ContentService.GetById(nodeId.Value);
            if (currentNode == null)
            {
                return Json(new PlaceholderModel());
            }

            var page = FindPage(currentNode);
            if (page == null)
            {
                return Json(new PlaceholderModel());
            }

            if (!page.TemplateId.HasValue)
            {
                return Json(new PlaceholderModel());
            }

            var template = Services.ContentTypeService.Get(page.ContentTypeId).AllowedTemplates.FirstOrDefault(x => x.Id == page.TemplateId.Value);
            if (template == null)
            {
                return Json(new PlaceholderModel());
            }

            var matches = regex.Matches(template.Content);
            var model = new PlaceholderModel()
            {
                PageId = page.Id,
                TemplateId = template.Id,
                Placholders = matches.Cast<Match>().Select(x => x.Groups.Cast<Group>().Select(g => g.Value).LastOrDefault()).ToArray()
            };
            return Json(model);
        }

        private IContent FindPage(IContent content)
        {
            if (content == null)
            {
                return null;
            }

            if (content.ParentId == 0)
            {
                return null;
            }

            var parentContent = Services.ContentService.GetById(content.ParentId);
            if (parentContent == null)
            {
                return null;
            }

            var contentType = Services.ContentTypeService.Get(parentContent.ContentTypeId);
            if (contentType.ContentTypeComposition.Any(x => x.Alias == Constants.ComponentBaseDocumentAlias))
            {
                return FindPage(parentContent);
            }

            return parentContent;
        }
    }
}

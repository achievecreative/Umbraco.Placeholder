using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using AC.Placeholder.Extensions;
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
        [HttpGet]
        public JsonResult<PlaceholderModel> FindPlaceholder(int? nodeId)
        {
            if (!nodeId.HasValue)
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

            var resolver = DependencyResolver.Current.GetServices<IPlaceholderResolver>()
                .LastOrDefault();

            var placeholderModel = resolver?.Find(page.Id);

            if (placeholderModel == null)
            {
                return Json(new PlaceholderModel());
            }

            return Json(placeholderModel);
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

            var componentFolder = content.ContentType.Alias == Constants.ComponentFolderAlias ? content : Services.ContentService.GetAncestors(content).FirstOrDefault(x => x.ContentType.Alias == Constants.ComponentFolderAlias);
            if (componentFolder == null)
            {
                return null;
            }

            return Services.ContentService.GetById(componentFolder.ParentId);
        }
    }
}

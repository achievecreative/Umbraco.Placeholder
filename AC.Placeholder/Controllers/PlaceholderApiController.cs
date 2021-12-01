using System;
using System.Collections.Generic;
using System.Linq;
using AC.Placeholder.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Attributes;


namespace AC.Placeholder.Controllers
{
    [PluginController("AC")]
    public class PlaceholderApiController : UmbracoAuthorizedJsonController
    {
        private IPlaceholderResolver _placeholderResolver;
        private IContentService _contentService;

        public PlaceholderApiController(IPlaceholderResolver placeholderResolver, IContentService contentService)
        {
            _contentService = contentService;
            _placeholderResolver = placeholderResolver;
        }

        [HttpGet]
        public PlaceholderModel FindPlaceholder(int? nodeId)
        {
            if (!nodeId.HasValue)
            {
                return new PlaceholderModel();
            }

            var currentNode = _contentService?.GetById(nodeId.Value);
            if (currentNode == null)
            {
                return new PlaceholderModel();
            }

            var page = FindPage(currentNode);
            if (page == null)
            {
                return new PlaceholderModel();
            }

            var placeholderModel = _placeholderResolver?.Find(page.Id);

            if (placeholderModel == null)
            {
                return new PlaceholderModel();
            }

            return placeholderModel;
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

            var componentFolder = content.ContentType.Alias == Constants.ComponentFolderAlias ? content : _contentService.GetAncestors(content).FirstOrDefault(x => x.ContentType.Alias == Constants.ComponentFolderAlias);
            if (componentFolder == null)
            {
                return null;
            }

            return _contentService?.GetById(componentFolder.ParentId);
        }
    }
}

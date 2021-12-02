using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AC.Placeholder.Extensions;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace AC.Placeholder.Features.Panels
{
    public class PanelListService : IPanelListService
    {
        public int Order => Int32.MinValue;

        public PanelList Get(IPublishedContent content)
        {
            if (!content.IsComponent())
            {
                return null;
            }

            return new PanelList()
            {
                Columns = content.GetValue<int>("columns"),
                Items = content.Children.Select(x => new PanelItem()
                {
                    Image = x.GetMediaUrl("image"),
                    Title = x.GetValue<string>("title"),
                    Content = x.GetValue<string>("content")
                }),
                Styles = content.Styles()
            };
        }
    }
}
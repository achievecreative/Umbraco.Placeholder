using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AC.Placeholder.Features.Models;

namespace AC.Placeholder.Features.Panels
{
    public class PanelList : Component
    {
        public int Columns { get; set; }

        public IEnumerable<PanelItem> Items { get; set; }
    }
}
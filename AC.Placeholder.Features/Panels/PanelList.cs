using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AC.Placeholder.Features.Panels
{
    public class PanelList
    {
        public int Columns { get; set; }

        public IEnumerable<PanelItem> Items { get; set; }
    }
}
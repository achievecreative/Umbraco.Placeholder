using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AC.Placeholder.Features.Features.Models
{
    public class NavigationItem
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public bool Activate { get; set; }

        public IEnumerable<NavigationItem> Children { get; set; }
    }
}
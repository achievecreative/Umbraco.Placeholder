using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AC.Placeholder.Models
{
    public class PlaceholderModel
    {
        public int PageId { get; set; }

        public int TemplateId { get; set; }

        public string[] Placholders { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace TestApp2.Models
{
    public class SortLinkModel
    {
        public string LinkText { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public RouteValueDictionary RouteValues { get; set; }
        public SortDirection? SortDirection { get; set; }
    }
}
using TestApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp2.Filters
{
    public class JobseekersFilter : BaseFilter
    {
        public List<Experience> Experience { get; set; }
    }
}
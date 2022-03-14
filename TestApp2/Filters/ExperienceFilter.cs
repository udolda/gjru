using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApp2.Models;

namespace TestApp2.Filters
{
    public class ExperienceFilter : BaseFilter
    {
        public List<Experience> Experiences { get; set; }
    }
}
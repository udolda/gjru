using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApp2.Filters;

namespace TestApp2.Models
{
    public class Vacancy
    {
        public virtual long Id { get; set; }
        public virtual string VacancyName { get; set; }
        public virtual string VacancyDescription { get; set; }
        public virtual DateTime Starts { get; set; }
        public virtual DateTime Ends { get; set; }
        public virtual Company Company { get; set; }
        public virtual IList<Experience> Requirements { get; set; }
        public virtual User Creator { get; set; }
        public virtual Status Status { get; set; }
    }
}
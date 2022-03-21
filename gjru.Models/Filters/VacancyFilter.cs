using gjru.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gjru.Models.Filters
{
    public class VacancyFilter : BaseFilter
    {
        public List<Experience> Experience { get; set; }
        public List<Experience> SelectedExperience { get; set; }

        [Display(Name = "Диапазон начала:")]
        public DateRange StartDateRange { get; set; }

        [Display(Name = "Диапазон окончания:")]
        public DateRange EndDateRange { get; set; }
        public Company CompanyName { get; set; }
        public List<Status> Statuses { get; set; }
    }
}

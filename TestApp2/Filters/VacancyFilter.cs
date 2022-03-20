﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestApp2.Models;

namespace TestApp2.Filters
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
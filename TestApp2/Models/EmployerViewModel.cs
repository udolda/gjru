using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestApp2.Models
{
    public class VacancyViewModel : EntityModel<Vacancy>
    {
        [Display(Name = "Название вакансии")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Начало")]
        public DateTime Starts { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Окончание")]
        public DateTime Ends { get; set; }

        [Display(Name = "Выберите название компании")]
        public List<SelectListItem> Company { get; set; }
        public string SelectedCompany { get; set; }
        [Display(Name = "Выберите требуемые навыки из списка")]
        public List<SelectListItem> Experience { get; set; }
        public List<string> SelectedExperience { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Статус вакансии")]
        public string Status { get; set; }

        [Display(Name = "Если ваших навыков нет в списке, введите их через ;")]
        public string NewExperience { get; set; }
    }


    public class VacancyListViewModel : EntityModel<List<Vacancy>>
    {
        public IList<Vacancy> Vacancies { get; set; }
        public role Role { get; set; }
        
        public VacancyListViewModel()
        {
            Vacancies = new List<Vacancy>();
        }
    }
}
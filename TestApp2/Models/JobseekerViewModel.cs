using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestApp2.Models
{
    public class ProfileModel : EntityModel<Candidate>
    {
        [Display(Name = "Ваше ФИО")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "День рождения")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        //[DataType(DataType.Upload)]
        [Display(Name = "Фото")]
        public HttpPostedFileBase Photo { get; set; }

        [Display(Name = "Выберите свои навыки из списка")]
        public List<SelectListItem> Experience { get; set; }
        public List<string> SelectedExperience { get; set; }

        [Display(Name = "Если ваших навыков нет в списке, введите их через ;")]
        public string NewExperience { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Фото")]
        public BinaryFile File { get; set; }
    }

    public class ProfileListViewModel : EntityModel<List<Candidate>>
    {
        public IList<Candidate> Profiles { get; set; }
        public ProfileListViewModel()
        {
            Profiles = new List<Candidate>();
        }
    }
}
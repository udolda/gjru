using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp2.Models;
using TestApp2.Repository;

namespace TestApp2.Controllers
{
    public class BaseController : Controller
    {
        protected UserRepository userRepository;
        protected ExperienceRepository experienceRepository;
        protected UserManager _userManager;

        public BaseController(UserRepository userRepository, ExperienceRepository experienceRepository)
        {
            this.userRepository = userRepository;
            this.experienceRepository = experienceRepository;
        }

        public UserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<UserManager>(); }
        }

        public User CurrentUser
        {
            get { return userRepository.GetCurrentUser(User); }
        }

        /// <summary>
        /// Метод для получения списка опыта. Позволяет преобразовать Experience в SelectListItem
        /// для дальнейшей работы
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetExperienceLists()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var e in experienceRepository.GetAll())
            {
                SelectListItem item = new SelectListItem
                {
                    Text = e.Skill,
                    Value = e.Id.ToString()
                };
                listItems.Add(item);
            }
            return listItems;
        }
    }
}

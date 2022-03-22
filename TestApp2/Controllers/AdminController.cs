using gjru.Models.Models;
using gjru.Models.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp2.Models;
//using TestApp2.Repository;

namespace TestApp2.Controllers
{
    public class AdminController : BaseController
    {
        private EmployerRepository employerRepository;

        public AdminController(EmployerRepository employerRepository,
            UserRepository userRepository,
            ExperienceRepository experienceRepository)
            : base(userRepository, experienceRepository)
        {
            this.employerRepository = employerRepository;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Возвращает главное меню администратора
        /// </summary>
        /// <returns>Main view</returns>
        public ActionResult Main()
        {
            var role = UserManager.GetRoles(Convert.ToInt64(User.Identity.GetUserId())).SingleOrDefault();
            if (CurrentUser.Role != gjru.Models.Models.role.Admin)
                return RedirectToAction("AccessError", "Common");

            return View();
        }

        /// <summary>
        /// Метод отображения списка пользователей
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public ActionResult ShowUsers(FetchOptions options)
        {
            var role = UserManager.GetRoles(Convert.ToInt64(User.Identity.GetUserId())).SingleOrDefault();
            if (CurrentUser.Role != gjru.Models.Models.role.Admin)
                return RedirectToAction("AccessError", "Common");

            var model = new UserListViewModel
            {
                Users = userRepository.GetAllWithSort(options)
            };

            return View(model);
        }

        /// <summary>
        /// Метод передает в представление данные о пользователе для его изменения
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult EditUser(long Id)
        {
            var current = userRepository.Load(Id);
            var model = new RegistrationViewModel
            {
                Entity = current,
                Email = current.UserName,
                Password = current.Password,
                Role = current.Role
                //PhoneNumber = current.PhoneNumber
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(RegistrationViewModel model)
        {
            var user = new User
            {
                Id = model.Id,
                Password = UserManager.PasswordHasher.HashPassword(model.Password),
                //PhoneNumber = model.PhoneNumber,
                Role = model.Role,
                UserName = model.Email
            };
            userRepository.Save(user);
            return RedirectToAction("ShowUsers", "Admin");
        }

        /// <summary>
        /// Метод для блокировки пользователей
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangeStatus()
        {
            var Id = Convert.ToInt64(User.Identity.GetUserId());
            var user = userRepository.Load(Id);
            if (user.Status == Status.Active)
            {
                user.Status = Status.Blocked;
            }
            else
            {
                user.Status = Status.Active;
            }
            userRepository.Save(user);
            return RedirectToAction("Main", "Admin");
        }

    }
}

using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp2.Repository;

namespace TestApp2.Controllers
{
    public class HRController : BaseController
    {
        private EmployerRepository employerRepository;
        private JobseekerRepository jobseekerRepository;
        private CompanyRepository companyRepository;

        public HRController(EmployerRepository employerRepository,
            JobseekerRepository jobseekerRepository,CompanyRepository companyRepository, UserRepository userRepository,
            ExperienceRepository experienceRepository)
            : base(userRepository, experienceRepository)
        {
            this.employerRepository = employerRepository;
            this.jobseekerRepository = jobseekerRepository;
            this.companyRepository = companyRepository;
        }

        /// <summary>
        /// Возвращает главное меню рекрутера
        /// </summary>
        /// <returns>Main view</returns>
        public ActionResult Main()
        {
            return View();
        }

    }
}

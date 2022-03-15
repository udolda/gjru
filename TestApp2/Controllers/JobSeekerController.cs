using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestApp2.Controllers
{
    public class JobSeekerController : Controller
    {
        /// <summary>
        /// Возвращает главное меню соискателя
        /// </summary>
        /// <returns>Main view</returns>
        public ActionResult Main()
        {
            return View();
        }

        /// <summary>
        /// Возвращает страницу редактирования профиля соискателя
        /// </summary>
        /// <returns>Edit profile view</returns>
        public ActionResult EditProfile()
        {
            return View();
        }

        // GET: JobSeeker/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobSeeker/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JobSeeker/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JobSeeker/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JobSeeker/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JobSeeker/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

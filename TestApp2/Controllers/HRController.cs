using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestApp2.Controllers
{
    public class HRController : Controller
    {
        /// <summary>
        /// Возвращает главное меню рекрутера
        /// </summary>
        /// <returns>Main view</returns>
        public ActionResult Main()
        {
            return View();
        }

        // GET: HR/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HR/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HR/Create
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

        // GET: HR/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HR/Edit/5
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

        // GET: HR/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HR/Delete/5
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

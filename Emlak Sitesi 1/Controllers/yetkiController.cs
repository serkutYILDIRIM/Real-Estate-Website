using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak_Sitesi_1.Controllers
{
    public class yetkiController : Controller
    {
        // GET: yetki
        public ActionResult Index()
        {
            return View();
        }

        // GET: yetki/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: yetki/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: yetki/Create
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

        // GET: yetki/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: yetki/Edit/5
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

        // GET: yetki/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: yetki/Delete/5
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

using Emlak_Sitesi_1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak_Sitesi_1.Controllers
{
    public class ResimController : Controller
    {
        // GET: Resim
        public ActionResult Index()
        {
            return View();
        }

        // GET: Resim/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Resim/Create
        public ActionResult Create()
        {
            return View();
        }
        EmlakEntities1 db = new EmlakEntities1();


        // POST: Resim/Create
        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public ActionResult Create(Resim resim, int id)
        {
            try
            {

                if (resim.yol.ContentLength > 0)
                {

                    var dosyaAdi = Path.GetFileName(resim.yol.FileName);
                    var kayıtYeri = Path.Combine(Server.MapPath("~/Content/Images/"), dosyaAdi);
                    resim.yol.SaveAs(kayıtYeri);
                    resim.ilanID = id;

                    resim.ResimYolu = "/Content/Images/" + dosyaAdi;
                    db.Resims.Add(resim);
                    db.SaveChanges();

                }

                return RedirectToAction("List", "ilan");
            }
            catch
            {
                return View();
            }
        }

        // GET: Resim/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Resim/Edit/5
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

        // GET: Resim/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Resim/Delete/5
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
        public ActionResult List(int id)
        {
            var resim = db.Resims.Where(i => i.ilanID == id).ToList();
            return View(resim);

        }
    }
}

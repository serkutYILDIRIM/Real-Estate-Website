using Emlak_Sitesi_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak_Sitesi_1.Controllers
{
    public class YorumController : Controller
    {

        EmlakEntities1 db = new EmlakEntities1();



        // GET: Yorum/Create
        public ActionResult Create(int id)
        {

            ViewBag.id = id;

            return View();
        }

        // POST: Yorum/Create
        [HttpPost]

        public ActionResult Create(yorum ekle, int id)
        {


            try
            {
                string kisiAd = Session["username"].ToString();
                var kisi = db.kullanicis.Where(i => i.kullaniciAdSoyad == kisiAd).SingleOrDefault().kullaniciID;
                ekle.ilanID = id;
                ekle.kullaniciID = kisi;
                db.yorums.Add(ekle);

                db.SaveChanges();

                return View();
            }

            catch
            {
                return View();
            }


        }



        // GET: Yorum/Delete/5
        public ActionResult Delete(int id)
        {
            var delete = db.yorums.Where(i => i.yorumID == id).SingleOrDefault();
            return View(delete);
        }

        // POST: Yorum/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, yorum sil)
        {
            try
            {
                var delete = db.yorums.Where(i => i.yorumID == id).SingleOrDefault();
                var ilanımm = db.yorums.Where(i => i.yorumID == id).SingleOrDefault().ilanID;
                db.yorums.Remove(delete);
                db.SaveChanges();
                return RedirectToAction("Details", "ilan", new { id = ilanımm });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult List(int id)
        {

            var ilanList = db.yorums.Where(i => i.ilanID == id).ToList();

            return View(ilanList);

        }
    }
}

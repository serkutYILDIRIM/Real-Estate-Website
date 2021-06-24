using Emlak_Sitesi_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak_Sitesi_1.Controllers
{
    public class kullaniciController : Controller
    {
        // GET: kullanici
        public ActionResult Index()
        {
            return View("Index");
        }
        EmlakEntities1 db = new EmlakEntities1();
        // GET: kullanici/Details/5
        public ActionResult Details(int id)
        {

            var kullanici = db.kullanicis.Where(i => i.kullaniciID == id).SingleOrDefault();

            return View(kullanici);
        }

        public ActionResult Profils()
        {

            string kullanıcıAd = Session["username"].ToString();
            var kisi = db.kullanicis.Where(i => i.kullaniciAdSoyad == kullanıcıAd).SingleOrDefault<kullanici>();

            return View(kisi);
        }

        // GET: kullanici/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }
        // GET: kullanici/Login
        [HttpPost]
        public ActionResult Login(kullanici kullanici)
        {

            var giris = db.kullanicis.Where(i => i.kullaniciAd == kullanici.kullaniciAd && i.kullaniciSifre == kullanici.kullaniciSifre).SingleOrDefault();
            if (giris != null)
            {
                Session["username"] = giris.kullaniciAdSoyad;
                return RedirectToAction("listele", "ilan");
            }
            return View();
        }

        // GET: kullanici/Login
        public ActionResult LoginOut()
        {
            Session["username"] = null;
            return RedirectToAction("listele", "ilan");
        }

        // POST: kullanici/Create
        [HttpPost]
        public ActionResult Create(kullanici yeni)
        {

            try
            {
                var ekle = db.kullanicis.Where(i => i.kullaniciAd == yeni.kullaniciAd).SingleOrDefault();
                if (ekle != null)
                {
                    return View();
                }

                if (string.IsNullOrEmpty(yeni.kullaniciSifre))
                {
                    return View();
                }
                yeni.yetkiID = 1;
                db.kullanicis.Add(yeni);
                db.SaveChanges();
                Session["username"] = yeni.kullaniciAdSoyad;

                return RedirectToAction("Index", "Kullanici");
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: kullanici/Edit/5
        public ActionResult Edit(int id)
        {
            var kisi = db.kullanicis.Where(i => i.kullaniciID == id).SingleOrDefault();

            return View(kisi);
        }

        // POST: kullanici/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, kullanici kullanici)
        {
            try
            {
                var kisi = db.kullanicis.Where(i => i.kullaniciID == id).SingleOrDefault();
                kisi.kullaniciAdSoyad = kullanici.kullaniciAdSoyad;
                kisi.kullaniciSifre = kullanici.kullaniciSifre;
                kisi.kullaniciMail = kullanici.kullaniciMail;
                db.SaveChanges();
                return RedirectToAction("Profils");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UserEdit(int id)
        {
            ViewBag.yetkiID = new SelectList(db.yetkis, "yetkiID", "yetkiAd");
            var kisi = db.kullanicis.Where(i => i.kullaniciID == id).SingleOrDefault();

            return View(kisi);
        }

        // POST: kullanici/Edit/5
        [HttpPost]
        public ActionResult UserEdit(int id, kullanici kullanici)
        {
            try
            {
                var kisi = db.kullanicis.Where(i => i.kullaniciID == id).SingleOrDefault();

                kisi.yetkiID = kullanici.yetkiID;
                db.SaveChanges();
                return RedirectToAction("Profils");
            }
            catch
            {
                return View();
            }
        }



        // GET: kullanici/Delete/5
        public ActionResult Delete(int id)
        {
            var kisi = db.kullanicis.Where(i => i.kullaniciID == id).SingleOrDefault();
            return View(kisi);
        }

        // POST: kullanici/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, kullanici kullanici)
        {
            try
            {
                var kisiler = db.kullanicis.Where(i => i.kullaniciID == id).SingleOrDefault();
                db.kullanicis.Remove(kisiler);
                db.SaveChanges();

                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult list()
        {
            var kisiler = db.kullanicis.Where(i => i.kullaniciID != 0).ToList().OrderByDescending(i => i.yetki.yetkiID);
            return View(kisiler);
        }
    }
}

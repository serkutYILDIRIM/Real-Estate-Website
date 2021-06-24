using Emlak_Sitesi_1.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak_Sitesi_1.Controllers
{
    public class ilanController : Controller
    {
        EmlakEntities1 db = new EmlakEntities1();
        // GET: ilan
        public ActionResult listele(int ilanOdaSayisi = 0, double ilanFiyat = 0, int kategoriID = 0, int islemID = 0)
        {


            ViewBag.kategoriID = new SelectList(db.kategoris, "kategoriID", "kategoriAd");

            ViewBag.islemID = new SelectList(db.islems, "islemID", "islemAd");
            var ilanlar = (from a in db.ilans select a);


            //kategori dolu durumlarında
            if (kategoriID != 0 && ilanOdaSayisi == 0 && ilanFiyat == 0 && islemID == 0)
            {
                ilanlar = db.ilans.Where(i => i.kategoriID == kategoriID);
            }

            if (kategoriID != 0 && ilanOdaSayisi != 0 && ilanFiyat == 0 && islemID == 0)
            {
                ilanlar = db.ilans.Where(i => i.kategoriID == kategoriID && i.ilanOdaSayisi == ilanOdaSayisi);
            }
            if (kategoriID != 0 && ilanOdaSayisi != 0 && ilanFiyat != 0 && islemID == 0)
            {
                ilanlar = db.ilans.Where(i => i.kategoriID == kategoriID && i.ilanOdaSayisi == ilanOdaSayisi && i.ilanFiyat == ilanFiyat);
            }
            if (kategoriID != 0 && ilanOdaSayisi != 0 && ilanFiyat != 0 && islemID != 0)
            {
                ilanlar = db.ilans.Where(i => i.kategoriID == kategoriID && i.ilanOdaSayisi == ilanOdaSayisi && i.ilanFiyat == ilanFiyat && i.islemID == islemID);
            }


            //işlemin dolu olduğu durumlarda
            if (islemID != 0 && kategoriID == 0 && ilanOdaSayisi == 0 && ilanFiyat == 0)
            {
                ilanlar = db.ilans.Where(i => i.islemID == islemID);
            }
            if (islemID != 0 && kategoriID != 0 && ilanOdaSayisi == 0 && ilanFiyat == 0)
            {
                ilanlar = db.ilans.Where(i => i.islemID == islemID && i.kategoriID == kategoriID);
            }
            if (islemID != 0 && kategoriID != 0 && ilanOdaSayisi != 0 && ilanFiyat == 0)
            {
                ilanlar = db.ilans.Where(i => i.islemID == islemID && i.kategoriID == kategoriID && i.ilanOdaSayisi == ilanOdaSayisi);
            }


            //odasayısı dolu olduğunda
            if (ilanOdaSayisi != 0 && kategoriID == 0 && islemID == 0 && ilanFiyat == 0)
            {
                ilanlar = db.ilans.Where(i => i.ilanOdaSayisi == ilanOdaSayisi);
            }
            if (ilanOdaSayisi != 0 && kategoriID != 0 && islemID == 0 && ilanFiyat == 0)
            {
                ilanlar = db.ilans.Where(i => i.ilanOdaSayisi == ilanOdaSayisi && i.kategoriID == kategoriID);
            }
            if (ilanOdaSayisi != 0 && kategoriID != 0 && islemID != 0 && ilanFiyat == 0)
            {
                ilanlar = db.ilans.Where(i => i.ilanOdaSayisi == ilanOdaSayisi && i.kategoriID == kategoriID && i.islemID == islemID);
            }


            //fiyat
            if (ilanFiyat != 0 && kategoriID == 0 && islemID == 0 && ilanOdaSayisi == 0)
            {
                ilanlar = db.ilans.Where(i => i.ilanFiyat == ilanFiyat);
            }
            if (ilanFiyat != 0 && kategoriID != 0 && islemID == 0 && ilanOdaSayisi == 0)
            {
                ilanlar = db.ilans.Where(i => i.ilanFiyat == ilanFiyat && i.kategoriID == kategoriID);
            }
            if (ilanFiyat != 0 && kategoriID != 0 && islemID != 0 && ilanOdaSayisi == 0)
            {
                ilanlar = db.ilans.Where(i => i.ilanFiyat == ilanFiyat && i.kategoriID == kategoriID && i.islemID == islemID);
            }



            //ViewBag.ilanlar = ilanlar.OrderByDescending(i => i.ilanFiyat);

            return View(ilanlar.OrderByDescending(i => i.tarih));
        }


        public ActionResult Index()
        {


            return View();
        }

        // GET: ilan/Details/5
        public ActionResult Details(int id)
        {
            var ilanList = db.ilans.Where(i => i.ilanID == id).ToList();
            ViewBag.details = ilanList;
            return View();
        }

        // GET: ilan/Create
        public ActionResult Create()
        {
            ViewBag.kategoriID = new SelectList(db.kategoris, "kategoriID", "kategoriAd");
            ViewBag.islemID = new SelectList(db.islems, "islemID", "islemAd");
            return View();
        }

        // POST: ilan/Create
        [HttpPost]
        public ActionResult Create(ilan ilan)
        {
            try
            {
                string kullaniciAdi = Session["username"].ToString();

                var kullanici = db.kullanicis.Where(i => i.kullaniciAdSoyad == kullaniciAdi).SingleOrDefault();
                ilan.kullaniciID = kullanici.kullaniciID;
                ilan.tarih = DateTime.Now;
                db.ilans.Add(ilan);
                db.SaveChanges();

                return RedirectToAction("List", "ilan");
            }
            catch
            {
                return View();
            }
        }

        // GET: ilan/Edit/5
        public ActionResult Edit(int id)
        {
            var ilanım = db.ilans.Where(i => i.ilanID == id).SingleOrDefault();
            ViewBag.kategoriID = new SelectList(db.kategoris, "kategoriID", "kategoriAd");
            ViewBag.islemID = new SelectList(db.islems, "islemID", "islemAd");
            return View(ilanım);
        }

        // POST: ilan/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ilan ilan)
        {
            try
            {
                var ilanım = db.ilans.Where(i => i.ilanID == id).SingleOrDefault();
                ilanım.ilanAciklama = ilan.ilanAciklama;
                ilanım.ilanAdres = ilan.ilanAdres;
                ilanım.ilanBaslik = ilan.ilanBaslik;
                ilanım.ilanBinaEsyaliMi = ilan.ilanBinaEsyaliMi;
                ilanım.ilanBinaisitma = ilan.ilanBinaisitma;
                ilanım.ilanBinaKacinciKat = ilan.ilanBinaKacinciKat;
                ilanım.ilanBinaKat = ilan.ilanBinaKat;
                ilanım.ilanBinaYasi = ilan.ilanBinaYasi;
                ilanım.ilanFiyat = ilan.ilanFiyat;
                ilanım.ilanOdaSayisi = ilan.ilanOdaSayisi;

                ilanım.islemID = ilan.islemID;
                ilanım.kategoriID = ilan.kategoriID;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        // GET: ilan/Delete/5
        public ActionResult Delete(int id)
        {
            var sil = db.ilans.Where(i => i.ilanID == id).SingleOrDefault();

            return View(sil);
        }

        // POST: ilan/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ilan ilan)
        {

            try
            {
                var sil = db.ilans.Where(i => i.ilanID == id).SingleOrDefault();
                db.ilans.Remove(sil);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult List()
        {
            string kullaniciAd = Session["username"].ToString();
            var ilanList = db.kullanicis.Where(i => i.kullaniciAdSoyad == kullaniciAd).SingleOrDefault().ilans.ToList();

            return View(ilanList);

        }
    }
}

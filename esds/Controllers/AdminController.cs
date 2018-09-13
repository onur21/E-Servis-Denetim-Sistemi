using esds.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace esds.Controllers
{
    public class AdminController : Controller
    {
        EsdsEntities1 baglanti = new EsdsEntities1();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Urunler()
        {
            return View(baglanti.Urun.ToList());
        }
        public ActionResult UrunEklee()
        {
            ViewBag.Kategoriler = baglanti.Kategori.ToList();
            ViewBag.Markalar = baglanti.Marka.ToList();
            return View();

        }
        [HttpPost]
        public ActionResult UrunEklee(Urun urun)
        {
          
                baglanti.Urun.Add(urun);
                baglanti.SaveChanges();
                return View();
           

        }
        public ActionResult Markalar() {

            return View(baglanti.Marka.ToList());

        }
        public ActionResult MarkaEkle()
        {

            return View();

        }
        public ActionResult UrunSil(int id) {
            Urun urn = (from u in baglanti.Urun where u.Id == id select u).FirstOrDefault();
            return View(urn);

       }
        [HttpPost,ActionName("UrunSil")]
        public ActionResult UrunSilme(int idd) {
            Urun urn = (from u in baglanti.Urun where u.Id == idd select u).FirstOrDefault();
            int Id = urn.Id;
            baglanti.Urun.Remove(urn);
            baglanti.SaveChanges();
            return RedirectToAction("Urunler", new { id = Id });
        }
        public ActionResult MarkaGuncelle(int id)
        {
            Marka mrk = baglanti.Marka.Find(id);
            
            return View(mrk);

        }
        
        [HttpPost]

        public ActionResult MarkaEkle(Marka mrk)
        {
            try
            {
                baglanti.Marka.Add(mrk);
                baglanti.SaveChanges();
                return View();
            }
            catch { return View();
 }
           
        }
        public ActionResult Kategoriler()
        {
            return View(baglanti.Kategori.ToList());



        }
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori kategori)
        {
            try
            {
                baglanti.Kategori.Add(kategori);
                baglanti.SaveChanges();
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
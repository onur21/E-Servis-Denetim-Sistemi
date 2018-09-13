using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using esds.Models;

namespace esds.Controllers
{
    public class HomeController : Controller
    {
        EsdsEntities2 baglanti = new EsdsEntities2();

        // GET: Home
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Uye uye)
        {
            var login = baglanti.Uye.Where(u => u.KullaniciAdi == uye.KullaniciAdi).SingleOrDefault();
            if (login.KullaniciAdi == uye.KullaniciAdi && login.Sifre==uye.Sifre) 
            {
                Session["uyeid"] = login.UyeId;
                Session["uyeadi"] = login.KullaniciAdi;
                Session["yetkiid"] = login.YetkiId;
                return RedirectToAction("Index","Admin");
            }
            else
            {
                ModelState.AddModelError("KullaniciAdi","Lütfen Yeni Kullanıcı Oluşturunuz");
            return View();

            }
        }
        public ActionResult LogOut() {
            Session["uyeid"] = null;
            Session.Abandon();
                return RedirectToAction("Login","Home");
        }
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(Uye uye)
        {
            if(ModelState.IsValid)
            {
                uye.YetkiId = 2;
                baglanti.Uye.Add(uye);
                baglanti.SaveChanges();
                Session["uyeid"] = uye.UyeId;
                Session["kullaniciadi"] = uye.KullaniciAdi;
                return RedirectToAction("Login","Home");
            }
            return View();
        }
    }
}
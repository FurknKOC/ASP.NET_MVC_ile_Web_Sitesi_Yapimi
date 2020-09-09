using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teknomar.Models.EntityFramework;
using System.Web.Security;
using System.IO;

namespace Teknomar.Controllers
{
    public class AdminController : Controller
    {
        eTicaretEntities db = new eTicaretEntities();
        private string find;

        [Authorize]
        public ActionResult AdminIndex()
        {
            return View();
        }
        [Authorize]
        public ActionResult AnakartEkle()
        {
            return View();
        }
        [Authorize]
        public ActionResult AnakartListele()
        {
            var model = db.AnakartUrunler.ToList();
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AnakartEkle(AnakartUrunler anakart)
        {
            if (anakart.AnakartAdi!=null)
            {
                if (anakart.AnakartUrunID==0)
                {
                    db.AnakartUrunler.Add(anakart);
                    db.SaveChanges();
                    return RedirectToAction("AdminIndex");
                }
                else
                {
                    return RedirectToAction("AnakartIndex");
                }
                //string resim1DosyaYolu = Path.GetFileName(anakart.resim1);
                //var resim1YuklemeYeri = Path.Combine(Server.MapPath("~/images"),resim1DosyaYolu);
                //anakart.SaveAs(resim1YuklemeYeri);
                
            }
            else
            {
                return View();
            }
        }

        public ActionResult Guncelle(int id)
        {
            var model = db.AnakartUrunler.Find(id);
            return View("AnakartGuncelle",model);

        }
        public ActionResult AnakartGuncelle()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AnakartGuncelle(AnakartUrunler anakart)
        {
            if (anakart.AnakartAdi != null)
            {
                db.Entry(anakart).State = System.Data.Entity.EntityState.Modified;
                return RedirectToAction("AnakartListele");
            }
            else
            {
                return View();
            }
        }
        public ActionResult AnakartSil(int id)
        {
            
            var silinecekAnakart = db.AnakartUrunler.Find(id);
            if(silinecekAnakart == null)
            {
                return HttpNotFound();
            }
            db.AnakartUrunler.Remove(silinecekAnakart);
            db.SaveChanges();
            return RedirectToAction("AnakartListele");
        }

       

        public ActionResult Login()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Login(Adminler kullanici)
        {
            var kullaniciInDb = db.Adminler.FirstOrDefault(x => x.KullaniciAdi == kullanici.KullaniciAdi && x.Sifre == kullanici.Sifre);
            if (kullaniciInDb != null)
            {
                FormsAuthentication.SetAuthCookie(kullaniciInDb.KullaniciAdi, false);
                return RedirectToAction("AdminIndex", "Admin");
            }
            else
            {
                ViewBag.Mesaj = "Geçersiz Email veya Şifre";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
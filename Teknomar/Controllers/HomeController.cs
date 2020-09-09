using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teknomar.Models.EntityFramework;
using System.Web.Security;

namespace Teknomar.Controllers
{
    public class HomeController : Controller
    {
        eTicaretEntities db = new eTicaretEntities();
        // GET: Home

        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Anakart()
        {
            var model = db.AnakartUrunler.ToList();     
            return View(model);
        }
        public ActionResult Islemci()
        {
            return View();
        }
        public ActionResult EkranKarti()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Sepet()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Uyelik");
            }
        }

        public ActionResult AnakartID(int id)
        {
            var model = db.AnakartUrunler.Find(id);
            return View("AnakartUrunSayfasi", model);
            //return View("AnakartUrunSayfasi",db.AnakartUrunler.Where(p=>p.AnakartUrunID == id).FirstOrDefault());
        }
        [HttpGet]
        public ActionResult AnakartUrunSayfasi(AnakartUrunler anakart)
        {
            //var urun = Request.QueryString["ID"];
            //ViewBag.urunID = urun;
            //var model = db.AnakartUrunler.ToList();
            //return View("AnakartUrunSayfasi",model);
            if (anakart.AnakartAdi != null)
            {
                //db.Entry(anakart).State = System.Data.Entity.EntityState.Modified;
                //return RedirectToAction("AnakartListele");
                return View();
            }
            else
            {
                return View();
            }

        }

        public ActionResult YeniUyelik(Uyeler uye)
        {
            if (uye.Ad != null)
            {
                    db.Uyeler.Add(uye);
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Uyelik()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Uyelik(Uyeler uye)
        {
            
            if (uye.Email != null)
            {
                if (ModelState.IsValid)
                {
                    var obj = db.Uyeler.Where(a => a.Email.Equals(uye.Email) && a.Sifre.Equals(uye.Sifre)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UyeID.ToString();
                        Session["UyeAD"] = obj.Ad.ToString();
                        return RedirectToAction("Index", uye);
                    }
                }
                return View();
            }
            else
            {
                return View();
            }
        }

        public ActionResult Cikis()
        {
            Session.RemoveAll();
            return RedirectToAction("Index");
        }

    }
}
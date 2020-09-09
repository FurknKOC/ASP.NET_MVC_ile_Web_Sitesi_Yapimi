using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Teknomar.Models.EntityFramework;

namespace Teknomar.Controllers
{
    [Authorize(Roles ="A")]
    public class AdminsController : Controller
    {
        private eTicaretEntities db = new eTicaretEntities();

        // GET: Admins
        public ActionResult Index()
        {
            return View(db.Adminler.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adminler adminler = db.Adminler.Find(id);
            if (adminler == null)
            {
                return HttpNotFound();
            }
            return View(adminler);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminID,KullaniciAdi,Sifre,Role")] Adminler adminler)
        {
            if (adminler.KullaniciAdi != null)
            {
                if (ModelState.IsValid)
                {
                    db.Adminler.Add(adminler);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(adminler);
            }
            else
            {
                return View();
            }
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adminler adminler = db.Adminler.Find(id);
            if (adminler == null)
            {
                return HttpNotFound();
            }
            return View(adminler);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminID,KullaniciAdi,Sifre,Role")] Adminler adminler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminler);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Adminler adminler = db.Adminler.Find(id);
            //if (adminler == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(adminler);
            var silinecekKullanici = db.Adminler.Find(id);
            if (silinecekKullanici == null)
            {
                return HttpNotFound();
            }
            db.Adminler.Remove(silinecekKullanici);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adminler adminler = db.Adminler.Find(id);
            db.Adminler.Remove(adminler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

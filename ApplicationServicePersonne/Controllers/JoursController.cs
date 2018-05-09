using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApplicationServicePersonne.Models;

namespace ApplicationServicePersonne.Controllers
{
    public class JoursController : Controller
    {
        private PersonneServiceEntities db = new PersonneServiceEntities();

        // GET: Jours
        public ActionResult Index()
        {
            return View(db.Jours.ToList());
        }

        // GET: Jours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jour jour = db.Jours.Find(id);
            if (jour == null)
            {
                return HttpNotFound();
            }
            return View(jour);
        }

        // GET: Jours/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jours/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_jour,nom_jour,disponibilité_jour")] Jour jour)
        {
            if (ModelState.IsValid)
            {
                db.Jours.Add(jour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jour);
        }

        // GET: Jours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jour jour = db.Jours.Find(id);
            if (jour == null)
            {
                return HttpNotFound();
            }
            return View(jour);
        }

        // POST: Jours/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_jour,nom_jour,disponibilité_jour")] Jour jour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jour);
        }

        // GET: Jours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jour jour = db.Jours.Find(id);
            if (jour == null)
            {
                return HttpNotFound();
            }
            return View(jour);
        }

        // POST: Jours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jour jour = db.Jours.Find(id);
            db.Jours.Remove(jour);
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

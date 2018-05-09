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
    public class Heure_travailController : Controller
    {
        private PersonneServiceEntities db = new PersonneServiceEntities();

        // GET: Heure_travail
        public ActionResult Index()
        {
            var heure_travail = db.Heure_travail.Include(h => h.Employeur).Include(h => h.Jour);
            return View(heure_travail.ToList());
        }

        // GET: Heure_travail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Heure_travail heure_travail = db.Heure_travail.Find(id);
            if (heure_travail == null)
            {
                return HttpNotFound();
            }
            return View(heure_travail);
        }

        // GET: Heure_travail/Create
        public ActionResult Create()
        {
            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur");
            ViewBag.id_jour = new SelectList(db.Jours, "id_jour", "nom_jour");
            return View();
        }

        // POST: Heure_travail/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_heure,heure_debut,heure_fin,id_jour,id_employeur")] Heure_travail heure_travail)
        {
            if (ModelState.IsValid)
            {
                db.Heure_travail.Add(heure_travail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur", heure_travail.id_employeur);
            ViewBag.id_jour = new SelectList(db.Jours, "id_jour", "nom_jour", heure_travail.id_jour);
            return View(heure_travail);
        }

        // GET: Heure_travail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Heure_travail heure_travail = db.Heure_travail.Find(id);
            if (heure_travail == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur", heure_travail.id_employeur);
            ViewBag.id_jour = new SelectList(db.Jours, "id_jour", "nom_jour", heure_travail.id_jour);
            return View(heure_travail);
        }

        // POST: Heure_travail/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_heure,heure_debut,heure_fin,id_jour,id_employeur")] Heure_travail heure_travail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(heure_travail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur", heure_travail.id_employeur);
            ViewBag.id_jour = new SelectList(db.Jours, "id_jour", "nom_jour", heure_travail.id_jour);
            return View(heure_travail);
        }

        // GET: Heure_travail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Heure_travail heure_travail = db.Heure_travail.Find(id);
            if (heure_travail == null)
            {
                return HttpNotFound();
            }
            return View(heure_travail);
        }

        // POST: Heure_travail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Heure_travail heure_travail = db.Heure_travail.Find(id);
            db.Heure_travail.Remove(heure_travail);
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

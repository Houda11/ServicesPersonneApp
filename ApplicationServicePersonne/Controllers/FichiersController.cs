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
    public class FichiersController : Controller
    {
        private PersonneServiceEntities db = new PersonneServiceEntities();

        // GET: Fichiers
        public ActionResult Index()
        {
            var fichiers = db.Fichiers.Include(f => f.Employeur);
            return View(fichiers.ToList());
        }

        // GET: Fichiers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fichier fichier = db.Fichiers.Find(id);
            if (fichier == null)
            {
                return HttpNotFound();
            }
            return View(fichier);
        }

        // GET: Fichiers/Create
        public ActionResult Create()
        {
            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur");
            return View();
        }

        // POST: Fichiers/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_fichier,nom_fichier,fichier1,id_employeur")] Fichier fichier)
        {
            if (ModelState.IsValid)
            {
                db.Fichiers.Add(fichier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur", fichier.id_employeur);
            return View(fichier);
        }

        // GET: Fichiers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fichier fichier = db.Fichiers.Find(id);
            if (fichier == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur", fichier.id_employeur);
            return View(fichier);
        }

        // POST: Fichiers/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_fichier,nom_fichier,fichier1,id_employeur")] Fichier fichier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fichier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur", fichier.id_employeur);
            return View(fichier);
        }

        // GET: Fichiers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fichier fichier = db.Fichiers.Find(id);
            if (fichier == null)
            {
                return HttpNotFound();
            }
            return View(fichier);
        }

        // POST: Fichiers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fichier fichier = db.Fichiers.Find(id);
            db.Fichiers.Remove(fichier);
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

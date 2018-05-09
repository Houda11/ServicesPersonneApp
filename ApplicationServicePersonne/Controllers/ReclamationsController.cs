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
    public class ReclamationsController : Controller
    {
        private PersonneServiceEntities db = new PersonneServiceEntities();

        // GET: Reclamations
        public ActionResult Index()
        {
            var reclamations = db.Reclamations.Include(r => r.Employeur).Include(r => r.Client);
            return View(reclamations.ToList());
        }

        // GET: Reclamations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamation reclamation = db.Reclamations.Find(id);
            if (reclamation == null)
            {
                return HttpNotFound();
            }
            return View(reclamation);
        }

        // GET: Reclamations/Create
        public ActionResult Create()
        {
            ViewBag.id_client = new SelectList(db.Admins, "id_client", "nom_client");
            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur");
            return View();
        }

        // POST: Reclamations/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_reclamation,sujet_reclam,detail,id_client,id_employeur")] Reclamation reclamation)
        {
            if (ModelState.IsValid)
            {
                db.Reclamations.Add(reclamation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_client = new SelectList(db.Admins, "id_client", "nom_client", reclamation.id_client);
            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur", reclamation.id_employeur);
            return View(reclamation);
        }

        // GET: Reclamations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamation reclamation = db.Reclamations.Find(id);
            if (reclamation == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_client = new SelectList(db.Admins, "id_client", "nom_client", reclamation.id_client);
            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur", reclamation.id_employeur);
            return View(reclamation);
        }

        // POST: Reclamations/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_reclamation, sujet_reclam, detail, id_client, id_employeur")] Reclamation reclamation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reclamation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_client = new SelectList(db.Admins, "id_client", "nom_client", reclamation.id_client);
            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur", reclamation.id_employeur);
            return View(reclamation);
        }

        // GET: Reclamations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamation reclamation = db.Reclamations.Find(id);
            if (reclamation == null)
            {
                return HttpNotFound();
            }
            return View(reclamation);
        }

        // POST: Reclamations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reclamation reclamation = db.Reclamations.Find(id);
            db.Reclamations.Remove(reclamation);
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

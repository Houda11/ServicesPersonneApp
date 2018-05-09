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
    public class Rendez_vousController : Controller
    {
        private PersonneServiceEntities db = new PersonneServiceEntities();

        // GET: Rendez_vous
        public ActionResult Index()
        {
            var rendez_vous = db.Rendez_vous.Include(r => r.Client).Include(r => r.Employeur);
            return View(rendez_vous.ToList());
        }

        // GET: Rendez_vous/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rendez_vous rendez_vous = db.Rendez_vous.Find(id);
            if (rendez_vous == null)
            {
                return HttpNotFound();
            }
            return View(rendez_vous);
        }

        // GET: Rendez_vous/Create
        public ActionResult Create()
        {
            ViewBag.id_client = new SelectList(db.Clients, "id_client", "nom_client");
            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur");
            return View();
        }

        // POST: Rendez_vous/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_employeur,id_client,date_RDV,heure_RDV")] Rendez_vous rendez_vous)
        {
            if (ModelState.IsValid)
            {
                db.Rendez_vous.Add(rendez_vous);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_client = new SelectList(db.Clients, "id_client", "nom_client", rendez_vous.id_client);
            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur", rendez_vous.id_employeur);
            return View(rendez_vous);
        }

        // GET: Rendez_vous/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rendez_vous rendez_vous = db.Rendez_vous.Find(id);
            if (rendez_vous == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_client = new SelectList(db.Clients, "id_client", "nom_client", rendez_vous.id_client);
            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur", rendez_vous.id_employeur);
            return View(rendez_vous);
        }

        // POST: Rendez_vous/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_employeur,id_client,date_RDV,heure_RDV")] Rendez_vous rendez_vous)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rendez_vous).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_client = new SelectList(db.Clients, "id_client", "nom_client", rendez_vous.id_client);
            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur", rendez_vous.id_employeur);
            return View(rendez_vous);
        }

        // GET: Rendez_vous/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rendez_vous rendez_vous = db.Rendez_vous.Find(id);
            if (rendez_vous == null)
            {
                return HttpNotFound();
            }
            return View(rendez_vous);
        }

        // POST: Rendez_vous/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rendez_vous rendez_vous = db.Rendez_vous.Find(id);
            db.Rendez_vous.Remove(rendez_vous);
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

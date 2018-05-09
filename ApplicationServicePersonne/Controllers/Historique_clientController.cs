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
    public class Historique_clientController : Controller
    {
        private PersonneServiceEntities db = new PersonneServiceEntities();

        // GET: Historique_client
        public ActionResult Index()
        {
            var historique_client = db.Historique_client;
            return View(historique_client.ToList());
        }

        // GET: Historique_client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historique_client historique_client = db.Historique_client.Find(id);
            if (historique_client == null)
            {
                return HttpNotFound();
            }
            return View(historique_client);
        }

        // GET: Historique_client/Create
        public ActionResult Create()
        {
            ViewBag.id_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur");
            return View();
        }

        // POST: Historique_client/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_historique,id_note,date_hist,id_client")] Historique_client historique_client)
        {
            if (ModelState.IsValid)
            {
                db.Historique_client.Add(historique_client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(historique_client);
        }

        // GET: Historique_client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historique_client historique_client = db.Historique_client.Find(id);
            if (historique_client == null)
            {
                return HttpNotFound();
            }
            
            return View(historique_client);
        }

        // POST: Historique_client/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_historique,id_note,date_hist,id_client")] Historique_client historique_client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historique_client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(historique_client);
        }

        // GET: Historique_client/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historique_client historique_client = db.Historique_client.Find(id);
            if (historique_client == null)
            {
                return HttpNotFound();
            }
            return View(historique_client);
        }

        // POST: Historique_client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Historique_client historique_client = db.Historique_client.Find(id);
            db.Historique_client.Remove(historique_client);
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

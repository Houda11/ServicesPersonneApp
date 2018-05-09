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
    public class NotesController : Controller
    {
        private PersonneServiceEntities db = new PersonneServiceEntities();

        // GET: Notes
        public ActionResult Index()
        {
            var notes = db.Notes.Include(n => n.Employeur).Include(n => n.Type_note);
            return View(notes.ToList());
        }

        // GET: Notes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // GET: Notes/Create
        public ActionResult Create()
        {
            ViewBag.is_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur");
            ViewBag.id_TN = new SelectList(db.Type_note, "id_TN", "nom_type");
            return View();
        }

        // POST: Notes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_note,valeur,id_client,is_employeur,id_TN")] Note note)
        {
            if (ModelState.IsValid)
            {
                db.Notes.Add(note);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.is_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur", note.is_employeur);
            ViewBag.id_TN = new SelectList(db.Type_note, "id_TN", "nom_type", note.id_TN);
            return View(note);
        }

        // GET: Notes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.is_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur", note.is_employeur);
            ViewBag.id_TN = new SelectList(db.Type_note, "id_TN", "nom_type", note.id_TN);
            return View(note);
        }

        // POST: Notes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_note,valeur,id_client,is_employeur,id_TN")] Note note)
        {
            if (ModelState.IsValid)
            {
                db.Entry(note).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.is_employeur = new SelectList(db.Employeurs, "id_employeur", "nom_employeur", note.is_employeur);
            ViewBag.id_TN = new SelectList(db.Type_note, "id_TN", "nom_type", note.id_TN);
            return View(note);
        }

        // GET: Notes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = db.Notes.Find(id);
            db.Notes.Remove(note);
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

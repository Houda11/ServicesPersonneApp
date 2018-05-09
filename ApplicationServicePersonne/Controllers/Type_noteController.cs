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
    public class Type_noteController : Controller
    {
        private PersonneServiceEntities db = new PersonneServiceEntities();

        // GET: Type_note
        public ActionResult Index()
        {
            return View(db.Type_note.ToList());
        }

        // GET: Type_note/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_note type_note = db.Type_note.Find(id);
            if (type_note == null)
            {
                return HttpNotFound();
            }
            return View(type_note);
        }

        // GET: Type_note/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Type_note/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_TN,nom_type")] Type_note type_note)
        {
            if (ModelState.IsValid)
            {
                db.Type_note.Add(type_note);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(type_note);
        }

        // GET: Type_note/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_note type_note = db.Type_note.Find(id);
            if (type_note == null)
            {
                return HttpNotFound();
            }
            return View(type_note);
        }

        // POST: Type_note/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_TN,nom_type")] Type_note type_note)
        {
            if (ModelState.IsValid)
            {
                db.Entry(type_note).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(type_note);
        }

        // GET: Type_note/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_note type_note = db.Type_note.Find(id);
            if (type_note == null)
            {
                return HttpNotFound();
            }
            return View(type_note);
        }

        // POST: Type_note/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Type_note type_note = db.Type_note.Find(id);
            db.Type_note.Remove(type_note);
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

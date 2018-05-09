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
    public class Contact_adminController : Controller
    {
        private PersonneServiceEntities db = new PersonneServiceEntities();

        // GET: Contact_admin
        public ActionResult Index()
        {
            var contact_admin = db.Contact_admin.Include(c => c.Admin);
            return View(contact_admin.ToList());
        }

        // GET: Contact_admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact_admin contact_admin = db.Contact_admin.Find(id);
            if (contact_admin == null)
            {
                return HttpNotFound();
            }
            return View(contact_admin);
        }

        // GET: Contact_admin/Create
        public ActionResult Create()
        {
            ViewBag.id_admin = new SelectList(db.Admins, "id_admin", "email");
            return View();
        }

        // POST: Contact_admin/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_contact,nom_contact,prenom_contact,email_contact,sujet_contact,Contenu,id_admin")] Contact_admin contact_admin)
        {
            if (ModelState.IsValid)
            {
                db.Contact_admin.Add(contact_admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_admin = new SelectList(db.Admins, "id_admin", "email", contact_admin.id_admin);
            return View(contact_admin);
        }

        // GET: Contact_admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact_admin contact_admin = db.Contact_admin.Find(id);
            if (contact_admin == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_admin = new SelectList(db.Admins, "id_admin", "email", contact_admin.id_admin);
            return View(contact_admin);
        }

        // POST: Contact_admin/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_contact,nom_contact,prenom_contact,email_contact,sujet_contact,Contenu,id_admin")] Contact_admin contact_admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact_admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_admin = new SelectList(db.Admins, "id_admin", "email", contact_admin.id_admin);
            return View(contact_admin);
        }

        // GET: Contact_admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact_admin contact_admin = db.Contact_admin.Find(id);
            if (contact_admin == null)
            {
                return HttpNotFound();
            }
            return View(contact_admin);
        }

        // POST: Contact_admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact_admin contact_admin = db.Contact_admin.Find(id);
            db.Contact_admin.Remove(contact_admin);
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

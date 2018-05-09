using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApplicationServicePersonne.Models;

namespace ApplicationServicePersonne.Controllers
{
    public class EmployeursController : Controller
    {
        private PersonneServiceEntities db = new PersonneServiceEntities();

        // GET: Employeurs
        public ActionResult Index()
        {
            var employeurs = db.Employeurs.Include(e => e.Service);
            return View(employeurs.ToList());
        }

        // GET: Employeurs/Details/5
        public ActionResult Details(int? id, HttpPostedFileBase Photos)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employeur employeur = db.Employeurs.Find(id);
            if (employeur == null)
            {
                return HttpNotFound();
            }
            return View(employeur);
        }

        // GET: Employeurs/Create
        public ActionResult Create()
        {
            ViewBag.id_service = new SelectList(db.Services, "id_service", "nom_service");
            return View();
        }

        // POST: Employeurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_employeur,nom_employeur,prenom_employeur,photo_employeur,email_employeur,sexe_employeur,mobile_employeur,ville,adresse,disponibilité_emp,password_employeur,titre_serv_emp,desc_serv_emp,prix,id_service")] Employeur employeur, HttpPostedFileBase Photos)
        {
            if (Photos.ContentLength > 0 && ModelState.IsValid)
            {
                
                string PhotosEmp = Path.GetFileName(Photos.FileName);
                string FolderPath = Path.Combine(Server.MapPath("~/Photos"), PhotosEmp);
                Photos.SaveAs(FolderPath);




                employeur.photo_employeur = PhotosEmp;
                db.Employeurs.Add(employeur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_service = new SelectList(db.Services, "id_service", "nom_service", employeur.id_service);
            return View(employeur);
        }

        // GET: Employeurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employeur employeur = db.Employeurs.Find(id);
            if (employeur == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_service = new SelectList(db.Services, "id_service", "nom_service", employeur.id_service);
            return View(employeur);
        }

        // POST: Employeurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_employeur,nom_employeur,prenom_employeur,photo_employeur,email_employeur,sexe_employeur,mobile_employeur,ville,adresse,disponibilité_emp,password_employeur,titre_serv_emp,desc_serv_emp,prix,id_service")] Employeur employeur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_service = new SelectList(db.Services, "id_service", "nom_service", employeur.id_service);
            return View(employeur);
        }

        // GET: Employeurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employeur employeur = db.Employeurs.Find(id);
            if (employeur == null)
            {
                return HttpNotFound();
            }
            return View(employeur);
        }

        // POST: Employeurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employeur employeur = db.Employeurs.Find(id);
            db.Employeurs.Remove(employeur);
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

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using ApplicationServicePersonne.Models;

namespace ApplicationServicePersonne.Controllers
{
    [AllowAnonymous]

    public class AccueilsController : Controller
    {

        private PersonneServiceEntities db = new PersonneServiceEntities();

        public ActionResult Accueil(string MotCle, string nomservice)
        {
            //to go to action liste recherche avec motcle
            if (ModelState.IsValid)
            {

                if (!string.IsNullOrEmpty(MotCle) || !string.IsNullOrEmpty(nomservice) )
                {

                    return RedirectToAction("Liste_recherche");
                }



            }

            //liste id service dans la table service employeur
            var ids_services_emp = (from Employeur in db.Employeurs
                                    join Service in db.Services on Employeur.id_service equals Service.id_service
                                    where Employeur.id_service == Service.id_service
                                    select Employeur.id_service);

            
            ViewBag.ids_services_emp = ids_services_emp;

            //les services a l'accueil
      
            
            return View(db.Services);

        }

       
        public ActionResult Liste_recherche(string MotCle, string nomservice)
            {
            var Liste_emp = (from Employeur in db.Employeurs select Employeur);

            if ((nomservice == "Catégorie") || (nomservice == "Not Selected"))
                nomservice = null;

            if (ModelState.IsValid)
            {
                    

                    if (!string.IsNullOrEmpty(MotCle) && !string.IsNullOrEmpty(nomservice))
                     {
                    //catégories des services && Motcle

                    Liste_emp = (from Employeur in db.Employeurs
                                 join Service in db.Services on Employeur.id_service equals Service.id_service
                                 where Service.nom_service == nomservice
                                 where Employeur.nom_employeur.Contains(MotCle) || Employeur.prenom_employeur.Contains(MotCle)
                                 select Employeur);
                     }
               
                     //catégories des services
                   else if (string.IsNullOrEmpty(MotCle) && !string.IsNullOrEmpty(nomservice))
                    {
                        Liste_emp = (from Employeur in db.Employeurs
                                     join Service in db.Services on Employeur.id_service equals Service.id_service
                                     where Service.nom_service == nomservice
                                     select Employeur);

                    }
                    else if (!string.IsNullOrEmpty(MotCle) && string.IsNullOrEmpty(nomservice))
                    {   //Mot clé
                  
                    Liste_emp = (from Employeur in db.Employeurs
                     where Employeur.nom_employeur.Contains(MotCle) || Employeur.prenom_employeur.Contains(MotCle)
                     select Employeur);
                    

                     }



            }
                

            var RstRecherche = Liste_emp.Count();
            ViewBag.rst = RstRecherche;

            int Npages;
            if (ViewBag.rst % 12 != 0)
            {
                Npages = (Int32)ViewBag.rst / 12 + 1;
            }
            else
            {
                Npages = (Int32)ViewBag.rst / 12;
            }


            ViewBag.Pagesmax = Npages;


            return View(Liste_emp);
        }

        //GET employeur
        public ActionResult RegisterEmployeur()
        {

            ViewBag.id_service = new SelectList(db.Services, "id_service", "nom_service");
            return View();
        }

        //Post employeur
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterEmployeur([Bind(Include = "id_employeur,nom_employeur,prenom_employeur,photo_employeur,email_employeur,sexe_employeur,mobile_employeur,ville,adresse,disponibilité_emp,password_employeur,titre_serv_emp,desc_serv_emp,prix,id_service")] Employeur employeur, HttpPostedFileBase Photos, string confpass)
        {
            


           

            if (Photos.ContentLength > 0 && ModelState.IsValid)
            {


                if (employeur.password_employeur == confpass)
                {






                    string PhotosEmp = Path.GetFileName(Photos.FileName);
                    string FolderPath = Path.Combine(Server.MapPath("~/Photos"), PhotosEmp);
                    Photos.SaveAs(FolderPath);




                    employeur.photo_employeur = PhotosEmp;
                    db.Employeurs.Add(employeur);
                    db.SaveChanges();
                    return RedirectToAction("Accueil");
            }
            else
            {
                ViewBag.msg = "retaper le mot de passe";
            }
        }

            ViewBag.id_service = new SelectList(db.Services, "id_service", "nom_service", employeur.id_service);
            return View(employeur);


        }
        
        //Get client
        public ActionResult RegisterClient()
        {
            return View();
        }

        //Post client
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterClient([Bind(Include = "id_client,nom_client,prenom_client,photo_client,email_client,sexe_client,mobile_client,password_client")] Client client, HttpPostedFileBase PhotosClients)
        {
            if (PhotosClients.ContentLength > 0 && ModelState.IsValid)
            {

                string PhotosClt = Path.GetFileName(PhotosClients.FileName);
                string FolderPath = Path.Combine(Server.MapPath("~/Photos"), PhotosClt);
                PhotosClients.SaveAs(FolderPath);

                client.photo_client = PhotosClt;
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Accueil");
            }

            return View(client);
        }

        public ActionResult Choix_emp_clt()
        {
            return View();
        }


        public ActionResult Contact()
        {

            return View();
        }

        public ActionResult Connexion()
        {
         
            return View();
        }

       
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var msg = "";

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {

                    //var emp = (from Employeur in db.Employeurs select Employeur);
                   var emp = (from Employeur in db.Employeurs
                            where Employeur.email_employeur.Equals(email) || Employeur.password_employeur.Contains(password)
                            select Employeur);

                    int empv = emp.Count();



                    //var clt = (from Client in db.Clients select Client);
                    var clt = (from Client in db.Clients
                           where Client.email_client.Equals(email) || Client.password_client.Contains(password)
                           select Client);

                    int cltv = clt.Count();


                    if (empv == 0 && cltv ==0)
                    {
                        msg = "utilisateur n'existe pas !";
                    }
                    else if (cltv != 0)
                    {
                        var UserAccount = (from Client in db.Clients
                                             where Client.email_client.Equals(email) || Client.password_client.Contains(password)
                                             select Client.id_client);
                        ViewBag.UserAccount = clt;
                        FormsAuthentication.SetAuthCookie(email,false);
                        return RedirectToAction("Liste_recherche", new { id = UserAccount });

                    }
                    else if (empv != 0)
                    {
                        var UserAccount = (from Employeur in db.Employeurs
                                           where Employeur.email_employeur.Equals(email) || Employeur.password_employeur.Contains(password)
                                           select Employeur.id_employeur);
                        ViewBag.UserAccount = emp;
                        FormsAuthentication.SetAuthCookie(email,false);
                        return RedirectToAction("ProfilEmployeur",new { id = UserAccount });


                    }

                    ViewBag.msg = msg;


                }
              
            }

            ViewBag.msg = msg;

           

            return View();
        }


        public ActionResult ProfilEmployeur(int? id , HttpPostedFileBase Photos)
        {
            id = 1;

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

        public ActionResult EditProfilEmployeur(int? id)
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfilEmployeur([Bind(Include = "id_employeur,nom_employeur,prenom_employeur,photo_employeur,email_employeur,sexe_employeur,mobile_employeur,ville,adresse,disponibilité_emp,password_employeur,titre_serv_emp,desc_serv_emp,prix,id_service")] Employeur employeur)
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

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Accueil");
        }


        public ActionResult Map()
        {
            return View();
        }

        public ActionResult A_propos()
        {

            return View();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DojoBO;
using Module06TP01.Data;
using Module06TP01.Models;

namespace Module06TP01.Controllers
{
    public class SamouraisController : Controller
    {
        private Module06TP01Context db = new Module06TP01Context();

        // GET: Samourais
        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }

        // GET: Samourais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            SamouraiViewModel vm = new SamouraiViewModel();
            vm.Armes = db.Armes.Select(x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() }).ToList();
            return View(vm);
        }

        // POST: Samourais/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SamouraiViewModel vm)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Samourai samourai = new Samourai();
                    samourai.Nom = vm.Samourai.Nom;
                    samourai.Force = vm.Samourai.Force;
                    samourai.Arme = db.Armes.Find(vm.IdArme);
                    db.Samourais.Add(samourai);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                vm.Armes = db.Armes.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();
                return View(vm);
            }
            catch (Exception)
            {
                vm.Armes = db.Armes.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();
                
                ModelState.AddModelError("", "Problème de connexion avec la base de données.");
                return View(vm);
            }

            
        }

        // GET: Samourais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            SamouraiViewModel vm = new SamouraiViewModel();
            vm.Samourai = samourai;
            vm.Armes = db.Armes.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();
            if (samourai.Arme != null)
            {
                vm.IdArme = samourai.Arme.Id;
            }
            
           
            if (samourai == null)
            {
                return HttpNotFound();
            }
            vm.Armes = db.Armes.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();
            return View(vm);
        }

        // POST: Samourais/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SamouraiViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // Lazy loading, demander explicitement, passer en eager
                Samourai samourai = db.Samourais.Include(x => x.Arme).FirstOrDefault(s => s.Id == vm.Samourai.Id);
                samourai.Nom = vm.Samourai.Nom;
                samourai.Force = vm.Samourai.Force;
                samourai.Arme = db.Armes.Find(vm.IdArme);

                

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            vm.Armes = db.Armes.Select(x => new SelectListItem() { Text = x.Nom, Value = x.Id.ToString() }).ToList();
            return View(vm);
        }

        // GET: Samourais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samourai samourai = db.Samourais.Find(id);
            db.Samourais.Remove(samourai);
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

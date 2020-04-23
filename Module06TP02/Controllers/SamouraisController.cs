using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DojoBO;
using Module06TP02.Data;
using Module06TP02.Models;

namespace Module06TP02.Controllers
{
    public class SamouraisController : Controller
    {
        private Module06TP02Context db = new Module06TP02Context();

 
        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }


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
            AddPotentialValue(samourai);
            return View(samourai);
        }


        public ActionResult Create()
        {
            SamouraiViewModel vm = new SamouraiViewModel();
            HydrateSamouraiViewModelLists(vm, null);
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
                    Samourai newSamourai = new Samourai();
                    newSamourai.Nom = vm.Samourai.Nom;
                    newSamourai.Force = vm.Samourai.Force;

                    // unicité de l'utilisation de l'arme
                    if (db.Armes.Find(vm.IdArme) != null)
                    {
                        foreach (Samourai samourai in db.Samourais.Include(s => s.Arme).Where(s => s.Arme.Id == vm.IdArme))
                        {
                            samourai.Arme = null;
                        }
                        newSamourai.Arme = db.Armes.Find(vm.IdArme);
                    }
                    if (vm.IdsArtMartials != null)
                    {
                        newSamourai.ArtMartiaux = db.ArtMartials.Where(am => vm.IdsArtMartials.Contains(am.Id)).ToList();
                    }
                    
                    db.Samourais.Add(newSamourai);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                HydrateSamouraiViewModelLists(vm, null);
                return View(vm);
            }
            catch (Exception)
            {
                HydrateSamouraiViewModelLists(vm, null);
                
                ModelState.AddModelError("", "Problème de contrainte avec la base de données.");
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
            AddPotentialValue(samourai);
            SamouraiViewModel vm = new SamouraiViewModel();
            vm.Samourai = samourai;
                if (samourai == null)
            {
                return HttpNotFound();
            }
            
            if (samourai.Arme != null)
            {
                vm.IdArme = samourai.Arme.Id;
            }

            if (samourai.ArtMartiaux != null)
            {
                vm.IdsArtMartials = samourai.ArtMartiaux.Select(a => a.Id).ToList();
            }

        
            HydrateSamouraiViewModelLists(vm, samourai);
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
                Samourai samouraiToUpdate = db.Samourais
                    .Include(x => x.Arme)
                    .Include(s => s.ArtMartiaux)
                    .FirstOrDefault(s => s.Id == vm.Samourai.Id);
                samouraiToUpdate.Nom = vm.Samourai.Nom;
                samouraiToUpdate.Force = vm.Samourai.Force;
                
                // unicité de l'arme
                if (db.Armes.Find(vm.IdArme) != null)
                {
                    foreach (Samourai samourai in db.Samourais.Include(s => s.Arme).Where(s => s.Arme.Id == vm.IdArme))
                    {
                        samourai.Arme = null;
                    }
                    samouraiToUpdate.Arme = db.Armes.Find(vm.IdArme);
                }

                if (vm.IdsArtMartials != null)
                {
                    samouraiToUpdate.ArtMartiaux = db.ArtMartials.Where(am => vm.IdsArtMartials.Contains(am.Id)).ToList();
                }

                db.Entry(samouraiToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            HydrateSamouraiViewModelLists(vm, db.Samourais.Include(s => s.Arme).FirstOrDefault(s => s.Id == vm.IdArme));
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
            db.Entry(samourai).State = EntityState.Deleted;
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

        private void AddPotentialValue(Samourai samourai)
        {
            samourai.Potentiel = (
                samourai.Force 
                + 
                (samourai.Arme != null ? samourai.Arme.Degats : 0)
                ) 
                * 
                (samourai.ArtMartiaux.Count() + 1);
        }

        private void HydrateSamouraiViewModelLists(SamouraiViewModel vm, Samourai editedSamourai = null)
        {
            // selection des armes non attribuées
            vm.Armes = db.Armes
                .Where(a => !db.Samourais.Select(s => s.Arme.Id).Contains(a.Id))
                .Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() })
                .ToList();
            // si un samourai est impliqué (edit), ajout de sa propre arme à la liste
            if (editedSamourai != null && editedSamourai.Arme != null)
            {
                vm.Armes.Add(new SelectListItem { Text = editedSamourai.Arme.Nom, Value = editedSamourai.Arme.Id.ToString() } );
            }

            // selection des arts martiaux.
            vm.ArtMartials = db.ArtMartials
                .Select(x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

            if (editedSamourai != null)
            {
                AddPotentialValue(vm.Samourai);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Module05TP03.Models;
using Module05TP03.Utils;
using PizzaEntities.Entities;

namespace Module05TP03.Controllers
{
    // variable statique : case mémoire alouée aux éléments de la variable.
    // maintient d'un espace mémoire, mapping par copie => double l'espace memoire.
    // commun à l'ensemble des instances de pizzacontroller, donc nouvelle instances => != singleton

    // static = fonction utilitaire ou bonne raison autre.
    public class PizzaController : Controller
    {
        // GET: Pizza
        public ActionResult Index()
        {
            return View(FakeDb.Instance.Pizzas);
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            PizzaViewModel vm = new PizzaViewModel();

            vm.Pates = FakeDb.Instance.PatesDisponible.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

            vm.Ingredients = FakeDb.Instance.IngredientsDisponible.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

            return View(vm);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaViewModel vm)
        {
            try
            {
                // cf prochain tp
                // ModelState.IsValid : ok système
                if (ModelState.IsValid && ValidateVM(vm))
                {
                    vm.Pates = FakeDb.Instance.PatesDisponible.Select(
              x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
              .ToList();

                    vm.Ingredients = FakeDb.Instance.IngredientsDisponible.Select(
                        x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                        .ToList();


                    Pizza pizza = vm.Pizza;

                    pizza.Pate = FakeDb.Instance.PatesDisponible.SingleOrDefault(x => x.Id == vm.IdPate);

                    pizza.Ingredients = FakeDb.Instance.IngredientsDisponible.Where(
                        x => vm.IdsIngredients.Contains(x.Id)) 
                        .ToList();
                    
                    // pizza requise
                    if (pizza.Pate == null)
                    {
                        ModelState.AddModelError("Pizza.Pate", "Le choix de la pate est requis.");
                        return View(vm);
                    }
                    // entre 2 et 5 ingredients
                    //if (pizza.Ingredients.Count() > 5 | pizza.Ingredients.Count() < 2)
                    //{
                    //    ModelState.AddModelError("Pizza.Ingredients", "Choisir entre 2 et 5 ingrédients.");
                    //    return View(vm);
                    //}
                    // nom pizza unique
                    //if (FakeDb.Instance.Pizzas.Any(x => x.Nom == pizza.Nom))
                    //{
                    //    ModelState.AddModelError("Pizza.Nom", "Ce nom de pizza existe déjà.");
                    //    return View(vm);
                    //}
                    // liste d'ingrédient unique
                    if (FakeDb.Instance.Pizzas.Any(x => x.Ingredients.SequenceEqual(pizza.Ingredients)))
                    {
                        ModelState.AddModelError("Pizza.Ingredients", "Une pizza possède déjà cette recette.");
                        return View(vm);
                    }


                    // cpu
                    pizza.Id = FakeDb.Instance.Pizzas.Count == 0 ? 1 : FakeDb.Instance.Pizzas.Max(x => x.Id) + 1;
                    // variable : ram

                    FakeDb.Instance.Pizzas.Add(pizza);

                    return RedirectToAction("Index");
                }
                else
                {
                    // propre : exception, mais exception spécifique.
                    // idem que create
                    vm.Pates = FakeDb.Instance.PatesDisponible.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

                    vm.Ingredients = FakeDb.Instance.IngredientsDisponible.Select(
                        x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                        .ToList();

                    return View(vm);
                }
            }
            catch
            {
                // idem que create
                vm.Pates = FakeDb.Instance.PatesDisponible.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

                vm.Ingredients = FakeDb.Instance.IngredientsDisponible.Select(
                    x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                    .ToList();

                return View(vm);
            }
        }

       private bool ValidateVM(PizzaViewModel vm)
        {
            bool result = true;
            return result;
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            PizzaViewModel vm = new PizzaViewModel();

            vm.Pates = FakeDb.Instance.PatesDisponible.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

            vm.Ingredients = FakeDb.Instance.IngredientsDisponible.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

            vm.Pizza = FakeDb.Instance.Pizzas.FirstOrDefault(x => x.Id == id);

            if (vm.Pizza.Pate != null)
            {

                vm.IdPate = vm.Pizza.Pate.Id;
            }

            if (vm.Pizza.Ingredients.Any())
            {
                vm.IdsIngredients = vm.Pizza.Ingredients.Select(x => x.Id).ToList();
            }

            return View(vm);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaViewModel vm)
        {
            try
            {
                // pizza issue de la liste des pizzas du singleton
                Pizza pizza = FakeDb.Instance.Pizzas.FirstOrDefault(x => x.Id == vm.Pizza.Id);
                // passage par référence, garder l'instance pizza du vm
                pizza.Nom = vm.Pizza.Nom;
                pizza.Pate = FakeDb.Instance.PatesDisponible.FirstOrDefault(x => x.Id == vm.IdPate);
                pizza.Ingredients = FakeDb.Instance.IngredientsDisponible.Where(x => vm.IdsIngredients.Contains(x.Id)).ToList();

                return RedirectToAction("Index");
            }
            catch
            {
                // vue graphique mais vide
                return View();
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            PizzaViewModel vm = new PizzaViewModel();
            vm.Pizza = FakeDb.Instance.Pizzas.FirstOrDefault(x => x.Id == id);
            return View(vm);
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Pizza pizza = FakeDb.Instance.Pizzas.FirstOrDefault(x => x.Id == id);
                FakeDb.Instance.Pizzas.Remove(pizza);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            PizzaViewModel vm = new PizzaViewModel();
            vm.Pizza = FakeDb.Instance.Pizzas.FirstOrDefault(x => x.Id == id);
            return View(vm);
        }
    }
}

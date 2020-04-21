using Module05TP02.Models;
using PizzaEntities.Entities;
using PizzaEntities.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Module05TP02.Controllers
{
    public class PizzaController : Controller
    {
        // GET: Pizza
        public ActionResult Index()
        {
            List<Pizza> pizzas = FakePizzaDB.Instance.Pizzas;
            return View(pizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            Pizza pizza = FakePizzaDB.Instance.Pizzas.FirstOrDefault(p => p.Id == id);
            return View(pizza);
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            PizzaVM pizzaVM = new PizzaVM { 
                Ingredients = FakePizzaDB.Instance.Ingredients, 
                Pates = FakePizzaDB.Instance.Pates };
            return View(pizzaVM);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaVM pizzaVM)
        {

            pizzaVM.Pizza.Ingredients = FakePizzaDB.Instance.Ingredients.Where(i => pizzaVM.IdsIngredients.Contains(i.Id)).ToList();
            pizzaVM.Pizza.Pate = FakePizzaDB.Instance.Pates.FirstOrDefault(p => p.Id == pizzaVM.IdPate);
            pizzaVM.Pizza.Nom = pizzaVM.Pizza.Nom;
            pizzaVM.Pizza.Id = FakePizzaDB.Instance.IdRange;

            FakePizzaDB.Instance.IdRange += 1;

            try
            {
                FakePizzaDB.Instance.Pizzas.Add(pizzaVM.Pizza);
                return RedirectToAction("Index"); 
            }
            catch
            {
                pizzaVM.Pates = FakePizzaDB.Instance.Pates;
                pizzaVM.Ingredients = FakePizzaDB.Instance.Ingredients;
                return View(pizzaVM);
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            PizzaVM pizzaVM = new PizzaVM { 
                Ingredients = FakePizzaDB.Instance.Ingredients, 
                Pates = FakePizzaDB.Instance.Pates , 
                Pizza = FakePizzaDB.Instance.Pizzas.FirstOrDefault(x => x.Id == id)};
            return View(pizzaVM);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaVM pizzaVM)
        {
            pizzaVM.Pizza.Ingredients = FakePizzaDB.Instance.Ingredients.Where(i => pizzaVM.IdsIngredients.Contains(i.Id)).ToList();
            pizzaVM.Pizza.Pate = FakePizzaDB.Instance.Pates.FirstOrDefault(p => p.Id == pizzaVM.IdPate);
            pizzaVM.Pizza.Nom = pizzaVM.Pizza.Nom;

            try
            {
                Pizza pizza = FakePizzaDB.Instance.Pizzas.FirstOrDefault(x => x.Id == pizzaVM.Pizza.Id);
                pizza.Nom = pizzaVM.Pizza.Nom;
                pizza.Ingredients = pizzaVM.Pizza.Ingredients;
                pizza.Pate = pizzaVM.Pizza.Pate;
                return RedirectToAction("Index");
            }
            catch
            {
                pizzaVM.Pates = FakePizzaDB.Instance.Pates;
                pizzaVM.Ingredients = FakePizzaDB.Instance.Ingredients;
                return View(pizzaVM);
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

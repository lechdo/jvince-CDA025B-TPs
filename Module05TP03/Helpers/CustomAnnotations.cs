using Module05TP03.Utils;
using PizzaEntities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Module05TP03.Helpers
{
    public class CustomAnnotations : ValidationAttribute
    {


        public static ValidationResult ListCountValidation(List<int> listeIngredient)
        {
            return listeIngredient.Count() >= 2 && listeIngredient.Count() <= 5 ?
                ValidationResult.Success : new ValidationResult("Choisir entre 2 et 5 ingrédients.");
        }


        public static ValidationResult UniquePizzaName(string pizzaName)
        {
            return FakeDb.Instance.Pizzas.Any(x => x.Nom == pizzaName) ? new ValidationResult("Ce nom de pizza existe déjà.")
                : ValidationResult.Success;
        }

        public static ValidationResult UniquePizzaListIngredient(List<int> listeIngredient)
        {
            return FakeDb.Instance.Pizzas.Any(x => x.Ingredients.Select(i => i.Id).SequenceEqual(listeIngredient)) ? new ValidationResult("Une pizza possède déjà cette recette.")
                : ValidationResult.Success;
        }
    }
}
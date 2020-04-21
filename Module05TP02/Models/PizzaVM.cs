using PizzaEntities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Module05TP02.Models
{
    public class PizzaVM
    {
        public List<Pate> Pates { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public Pizza Pizza { get; set; }

        public int IdPate { get; set; }

        public List<int> IdsIngredients { get; set; }

    }
}
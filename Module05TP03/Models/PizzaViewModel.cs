using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Module05TP03.Helpers;
using PizzaEntities;
using PizzaEntities.Entities;

namespace Module05TP03.Models
{
    public class PizzaViewModel 
    {

        public Pizza Pizza { get; set; }
        public List<SelectListItem> Ingredients { get; set; } = new List<SelectListItem>();

        
        public List<SelectListItem> Pates { get; set; } = new List<SelectListItem>();

    
        public int? IdPate { get; set; }


        [CustomValidation(typeof(CustomAnnotations), nameof(CustomAnnotations.ListCountValidation))]
        public List<int> IdsIngredients { get; set; } = new List<int>();
    }
}
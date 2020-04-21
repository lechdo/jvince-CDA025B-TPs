using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PizzaEntities.Entities
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required()]
        [StringLength(maximumLength: 20, MinimumLength = 5)]
        
        public string Nom { get; set; }
        public Pate Pate { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    }
}

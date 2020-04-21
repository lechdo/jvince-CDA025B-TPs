using PizzaEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaEntities.Utils
{
    public class FakePizzaDB
    {
        private static FakePizzaDB _instance;
        static readonly object instanceLock = new object();

        public int IdRange { get; set; }

        public List<Pate> Pates { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public List<Pizza> Pizzas { get; set; }

        private FakePizzaDB()
        {
            this.Pates = PatesDisponibles;
            this.Ingredients = IngredientsDisponibles;
            this.Pizzas = new List<Pizza>();
            this.IdRange = 1;
        }

        public static FakePizzaDB Instance
        {
            get
            {
                if (_instance == null) //Les locks prennent du temps, il est préférable de vérifier d'abord la nullité de l'instance.
                {
                    lock (instanceLock)
                    {
                        if (_instance == null) //on vérifie encore, au cas où l'instance aurait été créée entretemps.
                            _instance = new FakePizzaDB();
                    }
                }
                return _instance;
            }
        }

        private List<Ingredient> IngredientsDisponibles => new List<Ingredient>
        {
            new Ingredient{Id=1,Nom="Mozzarella"},
            new Ingredient{Id=2,Nom="Jambon"},
            new Ingredient{Id=3,Nom="Tomate"},
            new Ingredient{Id=4,Nom="Oignon"},
            new Ingredient{Id=5,Nom="Cheddar"},
            new Ingredient{Id=6,Nom="Saumon"},
            new Ingredient{Id=7,Nom="Champignon"},
            new Ingredient{Id=8,Nom="Poulet"}
        };

        private List<Pate> PatesDisponibles => new List<Pate>
        {
            new Pate{ Id=1,Nom="Pate fine, base crême"},
            new Pate{ Id=2,Nom="Pate fine, base tomate"},
            new Pate{ Id=3,Nom="Pate épaisse, base crême"},
            new Pate{ Id=4,Nom="Pate épaisse, base tomate"}
        };

    }
}

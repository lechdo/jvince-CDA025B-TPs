using Module05TP01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Module05TP01.Utils
{
    public class FakeDb
    {
        private static FakeDb _instance;
        static readonly object instanceLock = new object();

        public List<Chat> Chats { get; set; }

        private FakeDb()
        {
            this.Chats = GetMeuteDeChats();
        }

        public static FakeDb Instance
        {
            get
            {
                if (_instance == null) //Les locks prennent du temps, il est préférable de vérifier d'abord la nullité de l'instance.
                {
                    lock (instanceLock)
                    {
                        if (_instance == null) //on vérifie encore, au cas où l'instance aurait été créée entretemps.
                            _instance = new FakeDb();
                    }
                }
                return _instance;
            }
        }
        public List<Chat> GetMeuteDeChats()
        {
            var i = 1;
            return new List<Chat>
            {
                new Chat{Id=i++,Nom = "Felix",Age = 3,Couleur = "Roux"},
                new Chat{Id=i++,Nom = "Minette",Age = 1,Couleur = "Noire"},
                new Chat{Id=i++,Nom = "Miss",Age = 10,Couleur = "Blanche"},
                new Chat{Id=i++,Nom = "Garfield",Age = 6,Couleur = "Gris"},
                new Chat{Id=i++,Nom = "Chatran",Age = 4,Couleur = "Fauve"},
                new Chat{Id=i++,Nom = "Minou",Age = 2,Couleur = "Blanc"},
                new Chat{Id=i,Nom = "Bichette",Age = 12,Couleur = "Rousse"}
            };
        }
    }
}
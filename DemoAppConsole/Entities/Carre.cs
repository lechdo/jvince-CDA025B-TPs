using System;
using System.Text;

namespace DemoAppConsole
{
    public class Carre : Forme
    {
        public int Longueur { get; set; }

        public override double Aire => Math.Pow(Longueur, 2);

        public override double Perimetre => Longueur * 4;

        public override int GetHashCode()
        {
            int hash = base.GetHashCode();
            hash = hash * 31 + Longueur.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append($"Carré de coté {Longueur}")
                .Append(base.ToString())
                .ToString();
        }
    }
}
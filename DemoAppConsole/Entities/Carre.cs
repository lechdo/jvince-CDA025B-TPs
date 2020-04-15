using System;
using System.Text;

namespace DemoAppConsole
{
    public class Carre : Forme
    {
        public int Longueur { get; set; }

        public override double Aire()
        {
            return Math.Pow(Longueur,2);
        }

        public override double Perimetre()
        {
            return Longueur * 4;
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
using System;
using System.Text;

namespace DemoAppConsole
{
    public class Cercle : Forme
    {
        public int Rayon { get; set; }

        public override double Aire()
        {
            return Math.PI * Math.Pow(Rayon, 2); 
        }

        public override double Perimetre()
        {
            return 2* Math.PI * Rayon;
        }

        public override int GetHashCode()
        {
            int hash = base.GetHashCode();
            hash = hash * 31 + Rayon.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append($"Cercle de rayon {Rayon}")
                .Append(base.ToString())
                .ToString();
        }
    }
}
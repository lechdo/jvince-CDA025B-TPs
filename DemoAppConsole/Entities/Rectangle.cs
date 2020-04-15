using System.Text;

namespace DemoAppConsole
{
    internal class Rectangle : Forme
    {
        public int Largeur { get; set; }
        public int Longueur { get; set; }

        public override double Aire()
        { 
            return Largeur * Longueur;
        }

        public override double Perimetre()
        {
            return (Largeur * 2) + (Longueur * 2);
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append($"Rectangle de Longueur= {Longueur} et Largeur= {Largeur}")
                .Append(base.ToString())
                .ToString();
        }
    }
}
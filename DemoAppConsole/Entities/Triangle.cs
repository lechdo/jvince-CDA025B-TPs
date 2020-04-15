using System;
using System.Text;

namespace DemoAppConsole
{
    internal class Triangle : Forme
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }

        public override double Aire()
        {
            return (A * B) / 2; 
        }

        public override double Perimetre()
        {
            return A + B + C;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append($"Triangle de coté A= {this.A}, B= {this.B}, C= {this.C}")
                .Append(base.ToString())
                .ToString();
        }
    }
}
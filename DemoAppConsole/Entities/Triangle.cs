using System;
using System.Text;

namespace DemoAppConsole
{
    internal class Triangle : Forme
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }

        public override double Aire => (A * B) / 2;

        public override double Perimetre => A + B + C;

        public override int GetHashCode()
        {
            int hash = base.GetHashCode();
            hash = hash * 31 + A.GetHashCode();
            hash = hash * 31 + B.GetHashCode();
            hash = hash * 31 + C.GetHashCode();
            return hash;
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
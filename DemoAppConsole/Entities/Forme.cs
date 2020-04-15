using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace DemoAppConsole
{


    public abstract class Forme
    { 
        public const string AIRE = "Aire = {0}";
        public const string PERIMETRE = "Périmètre = {0}";

        public abstract double Aire { get; }

        public abstract double Perimetre { get; }

        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(Environment.NewLine)
                .Append(String.Format(AIRE, Aire))
                .Append(Environment.NewLine)
                .Append(String.Format(PERIMETRE, Perimetre))
                .Append(Environment.NewLine)
                .ToString();
        }


        

    }
}
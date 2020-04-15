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

        public abstract double Aire();
        public abstract double Perimetre();

        public override string ToString()
        {
            return new StringBuilder()
                .Append("\n")
                .Append(String.Format(AIRE, Aire()))
                .Append("\n")
                .Append(String.Format(PERIMETRE, Perimetre()))
                .Append("\n")
                .ToString();
        }


    }
}
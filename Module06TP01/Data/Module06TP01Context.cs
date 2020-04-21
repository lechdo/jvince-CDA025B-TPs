using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Module06TP01.Data
{
    public class Module06TP01Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Module06TP01Context() : base("name=Module06TP01Context")
        {
        }

        public System.Data.Entity.DbSet<DojoBO.Samourai> Samourais { get; set; }

        public System.Data.Entity.DbSet<DojoBO.Arme> Armes { get; set; }
    }
}

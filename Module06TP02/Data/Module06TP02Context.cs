using DojoBO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Web;

namespace Module06TP02.Data
{
    public class Module06TP02Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Module06TP02Context() : base("name=Module06TP02Context")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Samourai>().HasMany(s => s.ArtMartiaux).WithMany();
            modelBuilder.Entity<Samourai>().Ignore(s => s.Potentiel);
            //modelBuilder.Entity<Samourai>().HasOptional(s => s.Arme).WithOptionalDependent();

        }

        public System.Data.Entity.DbSet<DojoBO.Samourai> Samourais { get; set; }

        public System.Data.Entity.DbSet<DojoBO.Arme> Armes { get; set; }

        public DbSet<ArtMartial> ArtMartials { get; set; }
    }
}

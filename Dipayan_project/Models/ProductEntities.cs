using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Dipayan_project.Models
{
    public class ProductEntities:DbContext
    {
        public ProductEntities():base()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProductEntities,Dipayan_project.Migrations.Configuration>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ProductEntities>());
        }
        public DbSet<Category> category { get; set; }
        public DbSet<Product> product { get; set; }
    }
}
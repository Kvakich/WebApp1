using App.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.EF
{
    public class MobileContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }

        static MobileContext()
        {
            Database.SetInitializer<MobileContext>(new StoreDbInitializer());
        }
        public MobileContext(string connectionString)
            : base(connectionString)
        {
        }
    }

    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<MobileContext>
    {
        protected override void Seed(MobileContext db)
        {
            db.Phones.Add(new Phone { Name = "Nokia Lumia 630", Company = "Nokia", Price = 220, Data = 2000 });
            db.Phones.Add(new Phone { Name = "iPhone 6", Company = "Apple", Price = 320, Data = 2010 });
            db.Phones.Add(new Phone { Name = "LG G4", Company = "lG", Price = 260, Data = 2016 });
            db.Phones.Add(new Phone { Name = "Samsung Galaxy S 6", Company = "Samsung", Price = 300, Data = 2017 });
            db.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TokenAuthPractice.Models;

namespace TokenAuthPractice.DAL
{
    public class DataInitializer : System.Data.Entity. DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var products = new List<Product>
            {
                new Product { ID = 1,Name = "Headphone",Manufacturer = "Sony",price = 100}
            };
            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
            var users = new List<tblUser>
            {
                new tblUser {Id = 1, Username = "Edward", Password = "1234"}
            };
            users.ForEach(u => context.tblUsers.Add(u));
            context.SaveChanges();

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TokenAuthPractice.Models;

namespace TokenAuthPractice.DAL
{
    public class DataContext : DbContext
    {
       
       public DataContext() : base("DataContext")
        {

        }
      
        public DbSet<Product> Products { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<tblUser> tblUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
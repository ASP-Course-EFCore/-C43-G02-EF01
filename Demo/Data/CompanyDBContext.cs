using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data
{
    class CompanyDBContext:DbContext
    {
        //If there is Inheritance RelationShip
        //Any Constructor that i will make inside child class it chain by default on the constructor of the baseClass.
        public CompanyDBContext():base() 
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source = .; Initial Catalog = Company; UserId = sa; Password = 123456");//To Connect on sql server service using Sql Server Authentication using userName & password
            //optionsBuilder.UseSqlServer("Data Source = .; Initial Catalog = Company; Integrated Security = true");//To Connect on sql server using Windows Authentication using My Laptop/Server Name.
                                                                                                                    //This connection string syntax is legacy, so now they make new meaning full connection string
                                                                                                                    // but this still work

            optionsBuilder.UseSqlServer("Server = .; Database = Company; Trusted_Connection = true");//Trust App To connect on sql server service throw Windows authentication.
        }
        //So now the App is connected on sql server service throw my server/Laptop.
    }
}

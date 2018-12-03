using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TravelCompany.Models
{
    public class TravelContext: DbContext
    {
            static TravelContext()
            {
                Database.SetInitializer<TravelContext>(new DBInitializer());
            }

            public TravelContext() : base("name=myConnection")
            {

            }
        
            public virtual DbSet<Reservation> Reservations { get; set; }
            public virtual DbSet<Voyage> Voyages { get; set; }
            public virtual DbSet<Employee> Employees { get; set; }
    
    }
}
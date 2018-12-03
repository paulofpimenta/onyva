using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace TravelCompany.Models
{
    public class DBInitializer : DropCreateDatabaseAlways<TravelContext>
    {
        protected override void Seed(TravelContext context)
        {
            // Add employees
            var E1 = new Employee()
            {
                Nom = "John",
                Surname = "Coltrane",
                Salary = 2500,
                DateOfHire = new DateTime(1940, 12,10,12,45,44)
            };
            var E2 = new Employee()
            {
                Nom = "Jim",
                Surname = "Morrison",
                Salary = 3500,
                DateOfHire = new DateTime(1967, 05, 03, 08, 30, 19)

            };
            var E3 = new Employee()
            {
                Nom = "Chuck",
                Surname = "Berry",
                Salary = 2900,
                DateOfHire = new DateTime(1965, 02, 06, 07, 12, 01)

            };
            var E4 = new Employee()
            {
                Nom = "Lee",
                Surname = "Morgan",
                Salary = 2300,
                DateOfHire = new DateTime(1955, 07, 28, 20, 45, 43)

            };
            var E5 = new Employee()
            {
                Nom = "Dexter",
                Surname = "Gordon",
                Salary = 2600,
                DateOfHire = new DateTime(1960, 06, 20, 14, 50, 12)

            };
            var changes = context.ChangeTracker.Entries().ToList();

            context.Employees.Add(E1);
            context.Employees.Add(E2);
            context.Employees.Add(E3);
            context.Employees.Add(E4);
            changes = context.ChangeTracker.Entries().ToList();

            // Add voyages
            var V1 = new Voyage() { Destination = "Cancun" };
            var V2 = new Voyage() { Destination = "Rio de Janeiro" };
            var V3 = new Voyage() { Destination = "Madrid" };
            var V4 = new Voyage() { Destination = "London",
                DateOfVoyage = new DateTime(2000,07,5,12,45,44)};

            context.Voyages.Add(V1);
            context.Voyages.Add(V2);
            context.Voyages.Add(V3);
            context.Voyages.Add(V4);
            changes = context.ChangeTracker.Entries().ToList();

            // Add a reservation
            var R1 = new Reservation
            {
                ValidationState = ReservationStateEnum.InProgress,
                Voyage = V1,
                Employee = E2
            };

            context.Reservations.Add(R1);
            // Save changes to DB
            changes = context.ChangeTracker.Entries().ToList();
            context.SaveChanges();

        }
    }
}
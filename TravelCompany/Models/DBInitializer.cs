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
                Name = "John",
                Surname = "Coltrane",
                Salary = 2500,
                DateOfHire = new DateTime(2000, 12,10,12,45,44)
            };
            var E2 = new Employee()
            {
                Name = "Jim",
                Surname = "Morrison",
                Salary = 3500,
                DateOfHire = new DateTime(2001, 05, 03, 08, 30, 19)

            };
            var E3 = new Employee()
            {
                Name = "Chuck",
                Surname = "Berry",
                Salary = 2900,
                DateOfHire = new DateTime(2001, 02, 06, 07, 12, 01)

            };
            var E4 = new Employee()
            {
                Name = "Lee",
                Surname = "Morgan",
                Salary = 2300,
                DateOfHire = new DateTime(2015, 07, 28, 20, 45, 43)

            };
            var E5 = new Employee()
            {
                Name = "Dexter",
                Surname = "Gordon",
                Salary = 2600,
                DateOfHire = new DateTime(2018, 06, 20, 14, 50, 12)

            };
            var E6 = new Employee()
            {
                Name = "Gorge",
                Surname = "Clooney",
                Salary = 6600,
                DateOfHire = new DateTime(2017, 10, 09, 16, 14, 49)

            };
            var changes = context.ChangeTracker.Entries().ToList();

            context.Employees.Add(E1);
            context.Employees.Add(E2);
            context.Employees.Add(E3);
            context.Employees.Add(E4);
            context.Employees.Add(E5);
            context.Employees.Add(E6);
            changes = context.ChangeTracker.Entries().ToList();

            // Add voyages
            var V1 = new Voyage() { Destination = "Cancun" };
            var V2 = new Voyage() { Destination = "Rio de Janeiro" };
            var V3 = new Voyage() { Destination = "Madrid" };
            var V4 = new Voyage() { Destination = "London",
                DateOfVoyage = new DateTime(2018,07,5,12,45,44)};
            var V5 = new Voyage()
            {
                Destination = "Rome",
                DateOfVoyage = new DateTime(2010, 12, 31, 12, 30, 44)
            };
            context.Voyages.Add(V1);
            context.Voyages.Add(V2);
            context.Voyages.Add(V3);
            context.Voyages.Add(V4);
            context.Voyages.Add(V5);
            changes = context.ChangeTracker.Entries().ToList();

            // Add reservations
            var R1 = new Reservation
            {
                ValidationState = ReservationStateEnum.InProgress,
                Voyage = V1,
                Employee = E1
            };

            var R2 = new Reservation
            {
                ValidationState = ReservationStateEnum.InProgress,
                Voyage = V2,
                Employee = E2
            };

            var R3 = new Reservation
            {
                ValidationState = ReservationStateEnum.InProgress,
                Voyage = V3,
                Employee = E3
            };

            var R4 = new Reservation
            {
                ValidationState = ReservationStateEnum.InProgress,
                Voyage = V4,
                Employee = E4
            };

            var R5 = new Reservation
            {
                ValidationState = ReservationStateEnum.InProgress,
                Voyage = V5,
                Employee = E5
            };
            
            var R6 = new Reservation
            {
                ValidationState = ReservationStateEnum.InProgress,
                Voyage = V5,
                Employee = E4
            };

            var R7 = new Reservation
            {
                ValidationState = ReservationStateEnum.InProgress,
                Voyage = V5,
                Employee = E5
            };
            var R8= new Reservation
            {
                ValidationState = ReservationStateEnum.InProgress,
                Voyage = V5,
                Employee = E6
            };
            

            context.Reservations.Add(R1);
            context.Reservations.Add(R2);
            context.Reservations.Add(R3);
            context.Reservations.Add(R4);
            context.Reservations.Add(R5);
            context.Reservations.Add(R6);
            context.Reservations.Add(R7);
            context.Reservations.Add(R8);


            // Save changes to DB
            changes = context.ChangeTracker.Entries().ToList();
            context.SaveChanges();

        }
    }
}
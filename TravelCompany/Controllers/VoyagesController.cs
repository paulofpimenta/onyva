using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelCompany.Models;

namespace TravelCompany.Controllers
{
    public class VoyagesController : Controller
    {
        private TravelContext db = new TravelContext();

        // GET: Voyages
        public ActionResult Index()
        {
            return View(db.Voyages.ToList());
        }

        // GET: Voyages/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            return View(voyage);
        }

        // GET: Voyages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Voyages/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Destination,MaxNumVoyagers,DateOfVoyage,IsAvailable")] Voyage voyage)
        {
            if (ModelState.IsValid)
            {
                voyage.Id = Guid.NewGuid();
                db.Voyages.Add(voyage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(voyage);
        }

        // GET: Voyages/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            return View(voyage);
        }

        // POST: Voyages/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Destination,MaxNumVoyagers,DateOfVoyage,IsAvailable")] Voyage voyage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voyage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(voyage);
        }

        // GET: Voyages/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            return View(voyage);
        }

        // POST: Voyages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Voyage voyage = db.Voyages.Find(id);
            db.Voyages.Remove(voyage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Passengers(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            // List of reservations per voyage
            var reservations = db.Reservations.ToList();
            // List of Passagers per voyage
            if (reservations == null)
            {
                return HttpNotFound();
            }
            var employeeReservation = db.Employees.Where(e => e.IsInVoyage(voyage));

            ViewBag.Voyage = voyage;
            ViewBag.ReservationsToBeValidated = reservations
                         .Where(item => item.Voyage.Id == id)
                         .ToList();
            ViewBag.EmployesSubscribed = db.Employees.Where(e => e.IsInVoyage(voyage));

            return View(db.Employees.ToList());

        }


        public ActionResult PassengersInTravel(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Find(id);
            var employesInTravel = db.Employees.Where(e=> e.IsInVoyage(voyage));


            // List of reservations per voyage
            ViewBag.Reservations = db.Reservations
                         .Where(item => item.Voyage.Id == id)
                         .ToList();
            if (ViewBag.Reservations == null)
            {
                return HttpNotFound();
            }
            if (voyage == null)
            {
                return HttpNotFound();
            }
            ViewBag.Voyage = voyage;
            return View(db.Employees.ToList());

        }

        public ActionResult AddPassenger(Guid idVoyage, Guid idEmployee)
        {

            // Creation d'un objet Inscriptio
            var ReservationToAdd = new Reservation
            {
                Employee = db.Employees.Find(idEmployee),
                Voyage = db.Voyages.Find(idVoyage),
                ValidationState = ReservationStateEnum.InProgress
            };
            if (ReservationToAdd != null)
            {
                db.Reservations.Add(ReservationToAdd);
            }
            db.SaveChanges();
            return RedirectToAction("Passengers", new { id = idVoyage });
        }

        public ActionResult RemovePassenger(Guid idVoyage, Guid idEmployee)
        {
            var ReservationToDelete = db.Reservations.FirstOrDefault(c => c.Voyage.Id == idVoyage && c.Employee.Id == idEmployee);
            if (ReservationToDelete != null)
            {
                db.Reservations.Remove(ReservationToDelete);
            }

            db.SaveChanges();
            return RedirectToAction("Passengers", new { id = idVoyage });
        }

        public ActionResult ValidateVoyage(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            // Send a viewbag
            ViewBag.EmployeesInTrip = db.Reservations.Include(c => c.Employee).Where(c => c.Employee.Seniority >0);
            return View(voyage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

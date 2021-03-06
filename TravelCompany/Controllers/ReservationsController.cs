﻿using System;
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
    public class ReservationsController : Controller
    {
        private TravelContext db = new TravelContext();

        // GET: Reservations
        public ActionResult Index()
        {
            return View(db.Reservations.Include(c=>c.Employee).Include(c=>c.Voyage).ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reservations/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ValidationState")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.Id = Guid.NewGuid();
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            List<Employee> EmpList = db.Employees.ToList();
            ViewBag.ListOfValidEmployees = EmpList.Select(x => x.Credit >= 1).ToList();
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ValidationState")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Reservations/Delete/5
        public ActionResult ValidateReservation(Guid? idVoyage, Guid? idEmployee)
        {
            if (idVoyage == null || idEmployee == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reservation = db.Reservations.Where(c => c.Employee.Id == idEmployee && c.Voyage.Id == idVoyage);

            // Make a select with LINQ
            //var idReservation = reservation.Id;
            var Reservation = db.Reservations.FirstOrDefault(c => c.Employee.Id == idEmployee && c.Voyage.Id == idVoyage);

            if (Reservation == null)
            {
                return HttpNotFound();
            }
            var voyage = db.Voyages.Find(idVoyage);
            var employee = db.Employees.Find(idEmployee);

            // Validation rules

            //return Content("<html><body> id voyage : " + idVoyage + " id resev: " + Reservation.Id + " id emplooye : " + idEmployee + "</body></html>", "text/html");
            return RedirectToAction("Edit", new { id = Reservation.Id });
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

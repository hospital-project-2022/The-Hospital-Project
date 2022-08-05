using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HospitalProject.Models;
using System.Diagnostics;

namespace HospitalProject.Controllers
{
    public class AppointmentsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DepartmentData/ListDepartment
        [HttpGet]
        public IEnumerable<AppointmentsDto> ListAppointments()
        {
            List<Appointments> appointments = db.Appointment.ToList();
            List<AppointmentsDto> AppointmentsDtos = new List<AppointmentsDto>();

            appointments.ForEach(a => AppointmentsDtos.Add(new AppointmentsDto()
            {
                AppointmentsID = a.AppointmentsID,
                PatientID = a.PatientID,
                DoctorID = a.DoctorID,
                AppointmentsDateAndTime = a.AppointmentsDateAndTime
            }));

            return AppointmentsDtos;

        }

        // GET: api/AppointmentsData/FindAppointments/5
        [ResponseType(typeof(Appointments))]
        [HttpGet]
        public IHttpActionResult FindAppointments(int id)
        {
            Appointments appointments = db.Appointment.Find(id);
            AppointmentsDto AppointmentsDto = new AppointmentsDto()
            {
                AppointmentsID = appointments.AppointmentsID,
                PatientID = appointments.PatientID,
                DoctorID = appointments.DoctorID,
                AppointmentsDateAndTime = appointments.AppointmentsDateAndTime,
            };

            if (appointments == null)
            {
                return NotFound();
            }

            return Ok(AppointmentsDto);
        }

        // POST: api/AppointmentsData/UpdateAppointments/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateAppointments(int id, Appointments appointments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointments.AppointmentsID)
            {
                return BadRequest();
            }

            db.Entry(appointments).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AppointmentsData/AddAppointments
        [ResponseType(typeof(Appointments))]
        [HttpPost]
        public IHttpActionResult AddAppointments(Appointments appointments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Appointment.Add(appointments);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appointments.AppointmentsID }, appointments);
        }

        // POST: api/AppointmentsData/DeletAppointments/5
        [ResponseType(typeof(Appointments))]
        [HttpPost]
        public IHttpActionResult DeleteAppointments(int id)
        {
            Appointments appointments = db.Appointment.Find(id);
            if (appointments == null)
            {
                return NotFound();
            }

            db.Appointment.Remove(appointments);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppointmentsExists(int id)
        {
            return db.Appointment.Count(e => e.AppointmentsID == id) > 0;
        }
    }
}
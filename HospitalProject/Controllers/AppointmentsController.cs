using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using HospitalProject.Models;

namespace HospitalProject.Controllers
{
    public class AppointmentsController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static AppointmentsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44300/api/AppointmentsData/");
        }
        // GET: Appointments/
        public ActionResult Index()
        {
            return View();
        }

        // GET: Appointments/List
        public ActionResult List()
        {
            //curl https://localhost:44300/api/AppointmentsData/ListAppointments
            string url = "ListAppointments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<AppointmentsDto> appointments = response.Content.ReadAsAsync<IEnumerable<AppointmentsDto>>().Result;

            return View(appointments);
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int id, FormCollection form)
        {
            Debug.WriteLine("id" + id);
            Debug.WriteLine("form" + form["id"]);
            if (form["id"] != "")
            {
                id = Int32.Parse(form["id"]);
            }
            else
            {
                return View("Error");
            }

            Debug.WriteLine("id" + id);

            string url = "FindAppointments/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            AppointmentsDto selectedAppointments = response.Content.ReadAsAsync<AppointmentsDto>().Result;

            return View(selectedAppointments);
        }

        // GET: Appointments/New
        public ActionResult New()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        public ActionResult Create(Appointments appointments)
        {
            string url = "AddAppointments";


            string jsonpayload = jss.Serialize(appointments);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "FindAppointments/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AppointmentsDto selectedAppointment = response.Content.ReadAsAsync<AppointmentsDto>().Result;
            return View(selectedAppointment);
        }

        // POST: Appointments/Update/5
        [HttpPost]
        public ActionResult Update(int id, Appointments appointments)
        {
            string url = "UpdateAppointments/" + id;
            string jsonpayload = jss.Serialize(appointments);
            Debug.WriteLine("content" + jsonpayload);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return View("Index");
            }
            else
            {
                return View("Error");
            }
        }

        // GET: Appointments/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindAppointments/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AppointmentsDto selectedAppointment = response.Content.ReadAsAsync<AppointmentsDto>().Result;
            return View(selectedAppointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "DeleteAppointments/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}

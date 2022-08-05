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
    public class DoctorController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static DoctorController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44300/api/DepartmentData/");
        }
        // GET: Doctor/
        public ActionResult Index()
        {
            return View();
        }

        // GET: Doctor/List
        public ActionResult List()
        {
            //curl https://localhost:44300/api/DoctorData/ListDoctors
            string url = "ListDoctors";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DoctorDto> doctors = response.Content.ReadAsAsync<IEnumerable<DoctorDto>>().Result;

            return View(doctors);
        }

        // GET: Doctor/Details/5
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

            string url = "FindDoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DoctorDto selectedDoctor = response.Content.ReadAsAsync<DoctorDto>().Result;

            return View(selectedDoctor);
        }

        // GET: Doctor/New
        public ActionResult New()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        // POST: Doctor/Create
        [HttpPost]
        public ActionResult Create(Doctor doctor)
        {
            string url = "AddDoctor";


            string jsonpayload = jss.Serialize(doctor);

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

        // GET: Doctor/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "FindDoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DoctorDto selectedDoctor = response.Content.ReadAsAsync<DoctorDto>().Result;
            return View(selectedDoctor);
        }

        // POST: Doctor/Update/5
        [HttpPost]
        public ActionResult Update(int id, Doctor doctor)
        {
            string url = "UpdateDoctor/" + id;
            string jsonpayload = jss.Serialize(doctor);
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

        // GET: Doctor/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindDoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DoctorDto selectedDoctor = response.Content.ReadAsAsync<DoctorDto>().Result;
            return View(selectedDoctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "DeleteDoctor/" + id;
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

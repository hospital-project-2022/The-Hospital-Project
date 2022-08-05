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
    public class DepartmentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static DepartmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44300/api/DepartmentData/");
        }
        // GET: Department/
        public ActionResult Index()
        {
            return View();
        }

        // GET: Department/List
        public ActionResult List()
        {
            //curl https://localhost:44300/api/DepartmentData/ListDepartments
            string url = "ListDepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DepartmentDto> departments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;

            return View(departments);
        }

        // GET: Department/Details/5
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

            string url = "FindDepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DepartmentDto selectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;

            return View(selectedDepartment);
        }

        // GET: Department/New
        public ActionResult New()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(Department department)
        {
            string url = "AddDepartment";


            string jsonpayload = jss.Serialize(department);

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

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "FindDepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DepartmentDto selectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
            return View(selectedDepartment);
        }

        // POST: Department/Update/5
        [HttpPost]
        public ActionResult Update(int id, Department department)
        {
            string url = "UpdateDepartment/" + id;
            string jsonpayload = jss.Serialize(department);
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

        // GET: Department/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindDepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DepartmentDto selectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
            return View(selectedDepartment);
        }

        // POST: Department/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "DeleteDepartment/" + id;
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

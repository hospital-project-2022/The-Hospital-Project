﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalProject.Controllers
{
    public class PatientHistoryController : Controller
    {
        // GET: PatientHistory
        public ActionResult Index()
        {
            return View();
        }

        // GET: PatientHistory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PatientHistory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientHistory/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientHistory/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PatientHistory/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientHistory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatientHistory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

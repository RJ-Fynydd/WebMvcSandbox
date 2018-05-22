using System;
using System.Collections.Generic;
using WebMvcSandbox.Models;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.Mvc;
using WebMvcSandbox.Services;

namespace WebMvcSandbox.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            return View(EntryService.GetAllEntries());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddEntries()
        {
            return View();
        }

        public ActionResult UpdateEntries(int EntryId, string EntryName, string EntryNotes)
        {
            ViewBag.EntryId = EntryId;
            ViewBag.EntryName = EntryName;
            ViewBag.EntryNotes = EntryNotes;

            return View();
        }




        // POST 
        [HttpPost]
        public ActionResult AddEntries(string EntryName, string EntryNotes)
        {

            EntryService.AddEntry(new Entry(EntryName, EntryNotes));

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult UpdateEntriesPost(int EntryId, string EntryName, string EntryNotes, string action)
        {
            if (action.Equals("update"))
            {
                EntryService.UpdateEntry(new Entry(EntryId, EntryName, EntryNotes));
            }
            else
            {
                EntryService.RemoveEntry(EntryId);
            }

            return RedirectToAction("Index", "Home");
        }
        
    }
}
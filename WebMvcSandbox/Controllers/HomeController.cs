using System;
using System.Collections.Generic;
using WebMvcSandbox.Models;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.Mvc;

namespace WebMvcSandbox.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            List<Entry> entries = new List<Entry>();
            string constr = ConfigurationManager.ConnectionStrings["MySQL_Con"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT * FROM testtable";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            entries.Add(new Entry
                            {
                                entryId = Convert.ToInt32(sdr["entryId"]),
                                entryName = sdr["entryName"].ToString(),
                                entryNote = sdr["entryNote"].ToString()
                            });
                        }
                    }
                    con.Close();
                }

            }

            return View(entries);
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

        public ActionResult UpdateEntries(int entryId, string entryName, string entryNotes)
        {
            ViewBag.entryId = entryId;
            ViewBag.entryName = entryName;
            ViewBag.entryNotes = entryNotes;

            return View();
        }

        // POST 

        [HttpPost]
        public ActionResult AddEntries(string entryName, string entryNotes)
        {
            string constr = ConfigurationManager.ConnectionStrings["MySQL_Con"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "INSERT INTO testtable(entryName, entryNote) " +
                    "VALUES ('" + entryName + "','" + entryNotes + "')";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult UpdateEntriesPost(int entryId, string entryName, string entryNotes, string action)
        {
            if (action.Equals("update"))
            {
                string constr = ConfigurationManager.ConnectionStrings["MySQL_Con"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string query = "UPDATE testtable " +
                        "SET entryName = '" + entryName + "', entryNote = '" + entryNotes +
                        "' WHERE entryId = " + entryId;

                    using (MySqlCommand cmd = new MySqlCommand(query))
                    {
                        cmd.Connection = con;
                        con.Open();

                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                }
            
            }
            else
            {
                RemoveEntryPost(entryId);
            }

            return RedirectToAction("Index", "Home");
        }



        public void RemoveEntryPost(int entryId)
        {
            string constr = ConfigurationManager.ConnectionStrings["MySQL_Con"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "DELETE FROM testtable WHERE entryId = " + entryId;

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

    }
}
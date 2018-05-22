using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebMvcSandbox.Models;

namespace WebMvcSandbox.Services
{
    public class EntryService
    {

        public static List<Entry> GetAllEntries()
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
                                EntryId = Convert.ToInt32(sdr["entryId"]),
                                EntryName = sdr["entryName"].ToString(),
                                EntryNote = sdr["entryNote"].ToString()
                            });
                        }
                    }
                    con.Close();
                }

            }

            return entries;
        }

        public static Boolean AddEntry(Entry entry)
        {
            string constr = ConfigurationManager.ConnectionStrings["MySQL_Con"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "INSERT INTO testtable(entryName, entryNote) " +
                    "VALUES ('" + entry.EntryName + "','" + entry.EntryNote + "')";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }

            return true;

        }

        public static Boolean UpdateEntry(Entry entry)
        {
            string constr = ConfigurationManager.ConnectionStrings["MySQL_Con"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "UPDATE testtable " +
                    "SET entryName = '" + entry.EntryName + "', entryNote = '" + entry.EntryNote +
                    "' WHERE entryId = " + entry.EntryId;

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
            return true;
        }

        public static Boolean RemoveEntry(int entryId)
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

            return true;
        }

        public static Boolean AddTempEntry(string temp)
        {
            string constr = ConfigurationManager.ConnectionStrings["MySQL_Con"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "INSERT INTO temperature(DateTime, TempF) " +
                    "VALUES ('" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "'," + Convert.ToDouble(temp) + ")";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }

            return true;
        }


    }
}
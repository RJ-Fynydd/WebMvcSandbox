using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebMvcSandbox.Models;

namespace WebMvcSandbox.Services
{
    public class TemperatureService
    {

        public static Boolean AddTempEntry(Double temp)
        {
            string constr = ConfigurationManager.ConnectionStrings["MySQL_Con"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "INSERT INTO temperature(DateTime, TempF) " +
                    "VALUES ('" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "'," + temp + ")";

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


        public static IEnumerable<TemperatureEntry> GetTempEntries()
        {

            List<TemperatureEntry> entries = new List<TemperatureEntry>();

            string constr = ConfigurationManager.ConnectionStrings["MySQL_Con"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT * FROM temperature ORDER BY DateTime DESC LIMIT 100";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            entries.Add(new TemperatureEntry
                            {
                                EntryDateTime = (long)((DateTime)sdr["DateTime"]).ToUniversalTime().Subtract(
                                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds,
                                TempF = Convert.ToDouble(sdr["TempF"].ToString())
                            });

                        }
                    }
                    con.Close();
                }
            }

            return entries;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MySql.Data.MySqlClient;

namespace WebMvcSandbox.Models
{
    public class Entry
    {

        public int entryId { get; set; }
        public string entryName { get; set; }
        public string entryNote { get; set; }

    }
}
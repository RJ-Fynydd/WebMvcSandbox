using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MySql.Data.MySqlClient;

namespace WebMvcSandbox.Models
{
    public class Entry
    {

        public Entry(int EntryId, string EntryName, string EntryNotes = "")
        {
            this.EntryId = EntryId;
            this.EntryName = EntryName;
            this.EntryNote = EntryNotes;
        }


        public Entry(string EntryName, string EntryNotes = "")
        {
            this.EntryName = EntryName;
            this.EntryNote = EntryNotes;
        }


        public Entry() { }


        public int EntryId { get; set; }
        public string EntryName { get; set; }
        public string EntryNote { get; set; }

    }
}
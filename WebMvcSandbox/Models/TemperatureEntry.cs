using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvcSandbox.Models
{
    public class TemperatureEntry
    {
        public long EntryDateTime { get; set; }
        public double TempF { get; set; }
    }
}
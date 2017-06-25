using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinaryClinic.Web.Models
{
    public class ScoreByMonthViewModel
    {
        public string Category { get; set; }
        public int MaterialCosts { get; set; }
        public int Price { get; set; }
    }
}
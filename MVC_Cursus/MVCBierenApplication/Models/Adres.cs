using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBierenApplication.Models
{
    public class Adres
    {
        public string Naam { get; set; }
        public string Straat { get; set; }
        public int HuisNr { get; set; }
        public string PostCode { get; set; }
        public string Gemeente { get; set; }
        public string TelefoonNr { get; set; }
        public string EMail { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBierenApplication.Models
{
    public class Bier
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public float Alcohol { get; set; }
    }
}
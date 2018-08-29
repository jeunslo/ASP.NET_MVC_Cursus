using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCBierenApplication.Models
{
    public class Bier
    {
        [DisplayFormat(DataFormatString = "{0:###000}")]
        public int ID { get; set; }
        public string Naam { get; set; }

        [UIHint("AlcoholKleur")]
        public float Alcohol { get; set; }
    }
}
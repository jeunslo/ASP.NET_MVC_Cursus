using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCBierenSecurityAPP.Models
{
    public class BierProperties
    {
        [DisplayFormat(DataFormatString = "{0:000}")]
        public int BierNr { get; set; }

        [UIHint("AlcoholKleur")]
        [Range(0, 15, ErrorMessage = "{0} mag liggen tussen {1} en {2}")]
        public Nullable<float> Alcohol { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} mag maar {1} lang zijn")]
        public string Naam { get; set; }

        [ScaffoldColumn(false)]
        public int BrouwerNr { get; set; }

        [ScaffoldColumn(false)]
        public int SoortNr { get; set; }
    }
}
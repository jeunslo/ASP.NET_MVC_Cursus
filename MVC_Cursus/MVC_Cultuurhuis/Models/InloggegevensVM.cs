using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Cultuurhuis.Models
{
    public class InloggegevensVM
    {
        [Display(Name = "Gebruikersnaam")]
        public string UserName { get; set; }
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }
    }
}
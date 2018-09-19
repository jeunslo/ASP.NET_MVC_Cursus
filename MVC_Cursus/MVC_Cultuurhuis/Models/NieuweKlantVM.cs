using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Cultuurhuis.Models
{
    public class NieuweKlantVM
    {
        public int KlantNr { get; set; }
        [Required(ErrorMessage = "{0} is verplicht in te vullen!")]
        public string Voornaam { get; set; }
        [Required(ErrorMessage = "{0} is verplicht in te vullen!")]
        public string Familienaam { get; set; }
        [Required(ErrorMessage = "{0} is verplicht in te vullen!")]
        public string Straat { get; set; }
        [Required(ErrorMessage = "{0} is verplicht in te vullen!")]
        public string HuisNr { get; set; }
        [Required(ErrorMessage = "{0} is verplicht in te vullen!")]
        public string Postcode { get; set; }
        [Required(ErrorMessage = "{0} is verplicht in te vullen!")]
        public string Gemeente { get; set; }
        [Required(ErrorMessage = "{0} is verplicht in te vullen!")]
        [System.Web.Mvc.Remote("ValidateDoubleUsername", "Home")]
        //[DoubleUserName(ErrorMessage = "Een klant met deze gebruikersnaam komt al voor in de database. Kies een andere naam.")]
        public string GebruikersNaam { get; set; }

        [Display(Name = "Wachtwoord")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} is verplicht in te vullen!")]
        public string Paswoord { get; set; }

        [Display(Name = "Wachtwoord bevestigen")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} is verplicht in te vullen!")]
        [Compare("Paswoord", ErrorMessage = "Wachtwoord bevestigen verschilt van Wachtwoord. Probeer opnieuw.")]
        public string HerhaaldWachtwoord { get; set; }
    }
}
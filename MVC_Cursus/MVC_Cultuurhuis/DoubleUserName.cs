using MVC_Cultuurhuis.Models;
using MVC_Cultuurhuis.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Cultuurhuis
{
    public class DoubleUserName : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            if (!(value is string))
                return false;
            CultuurServices service = new CultuurServices();
            Klant klant = service.FindKlantByGebruikersnaam(value.ToString());

            if (klant != null)
                return false;
            else
                return true;
        }
    }
}
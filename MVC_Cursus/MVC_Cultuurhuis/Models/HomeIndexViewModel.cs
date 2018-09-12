using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Cultuurhuis.Models
{
    public class HomeIndexViewModel
    {
        public Genre Genre { get; set; }
        public List<Genre> GenreList { get; set; }
        public List<Voorstelling> VoorstellingsList { get; set; }
    }
}
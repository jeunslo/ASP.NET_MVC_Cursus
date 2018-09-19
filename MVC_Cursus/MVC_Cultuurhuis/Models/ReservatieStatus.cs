using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Cultuurhuis.Models
{
    public class ReservatieStatus
    {
        public List<Voorstelling> GelukteReservaties { get; set; }
        public List<Voorstelling> MislukteReservaties { get; set; }
        public List<Reservatie> Reservaties { get; set; }
    }
}
using System.Web.Mvc;

namespace MVC_Areas.Areas.Privé
{
    public class PrivéAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Privé";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Privé_default",
                "Privé/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "MVC_Areas.Areas.Privé.Controllers" }
            );
        }
    }
}
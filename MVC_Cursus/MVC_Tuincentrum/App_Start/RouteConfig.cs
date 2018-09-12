﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;
using System.Web.Mvc.Routing;

namespace MVC_Tuincentrum
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            var constraintsResolver = new DefaultInlineConstraintResolver();
            constraintsResolver.ConstraintMap.Add("BTW", typeof(ValuesConstraint));
            routes.MapMvcAttributeRoutes(constraintsResolver);
            //routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                "AllePlanten",
                "Plantenlijst",
                new { controller = "Plant", action = "Index" }
                );

            routes.MapRoute(
                "PlantNr",
                "Plant/{id}",
                new { controller = "Plant", action = "Details" },
                new { id = new IntRouteConstraint() }
                );

            routes.MapRoute(
                "PlantenVanEenLeverancier",
                "leverancier/{levnr}/planten",
                new { controller = "Plant", action = "FindPlantenByLeverancier" },
                new { levnr = new MaxRouteConstraint(10)}
                );

            routes.MapRoute(
                "PlantenVanEenSoort",
                "soort/{soortnaam}/planten",
                new { controller = "Plant", action = "FindPlantenBySoortNaam" },
                new { soortnaam = new CompoundRouteConstraint(new List<IRouteConstraint>
                        {
                            new AlphaRouteConstraint(),
                            new MinLengthRouteConstraint(3),
                            new MaxLengthRouteConstraint(10)
                        })
                    }
                );

            routes.MapRoute(
                "PantenMetEenPrijsRange",
                "planten",
                new { controller = "Plant", action = "FindPlantenBetweenPrijzen"},
                new { QueryConstraint = new QueryStringConstraint(
                    new string[] { "minprijs", "maxprijs" }
                    )}
                );

            routes.MapRoute(
                "PantenMetEenKleur",
                "planten",
                new { controller = "Plant", action = "FindPlantenVanEenKleur" },
                new
                {
                    QueryConstraint = new QueryStringConstraint(
                    new string[] { "kleur" }
                    )
                }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}

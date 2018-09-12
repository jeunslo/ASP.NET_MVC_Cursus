namespace MVCBierenSecurityAPP.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MVCBierenSecurityAPP.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCBierenSecurityAPP.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "MVCBierenSecurityAPP.Models.ApplicationDbContext";
        }

        protected override void Seed(MVCBierenSecurityAPP.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //if(!context.Users.Any(u => u.UserName == "Jeunsing@mvc.net"))
            //{
            //    var userStore = new UserStore<ApplicationUser>(context);
            //    var userManager = new UserManager<ApplicationUser>(userStore);

            //    var roleStore = new RoleStore<IdentityRole>(context);
            //    var roleManager = new RoleManager<IdentityRole>(roleStore);

            //    var user = new ApplicationUser { UserName = "Jeunsing@mvc.net" };
            //    userManager.Update(user, "Jeunsing1");

            //    var role = new IdentityRole { Name = "Administrator" };
            //    roleManager.Create(role);

            //    userManager.AddToRole(user.Id, "Administrator");
            //}

            //var user = context.Users.FirstOrDefault(u => u.UserName == "Jeunsing@mvc.net");
            //if(user!= null)
            //{
            //    user.PasswordHash = new PasswordHasher().HashPassword("Jeunsing1");
            //}
        }
    }
}

using LaraSotoProyecto.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LaraSotoProyecto.Startup))]
namespace LaraSotoProyecto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CrearRolUsuario();
        }

        private void CrearRolUsuario()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "admin@titulacion.com";
                user.Email = "admin@titulacion.com";
                string userPWD = "Beto123-";
                userManager.Create(user, userPWD);

                userManager.AddToRole(user.Id, "Admin");
            }
            if (!roleManager.RoleExists("Profesor"))
            {
                var role = new IdentityRole();
                role.Name = "Profesor";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "profesor@titulacion.com";
                user.Email = "profesor@titulacion.com";
                string userPWD = "Profesor123-";
                userManager.Create(user, userPWD);

                userManager.AddToRole(user.Id, "Profesor");
            }
            if (!roleManager.RoleExists("Admin2"))
            {
                var role = new IdentityRole();
                role.Name = "Admin2";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "admin2@titulacion.com";
                user.Email = "admin2@titulacion.com";
                string userPWD = "Admin123-";
                userManager.Create(user, userPWD);

                userManager.AddToRole(user.Id, "Admin2");
            }
            if (!roleManager.RoleExists("Estudiante"))
            {
                var role = new IdentityRole();
                role.Name = "Estudiante";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "estudiante@titulacion.com";
                user.Email = "estudiante@titulacion.com";
                string userPWD = "Estudiante123-";
                userManager.Create(user, userPWD);

                userManager.AddToRole(user.Id, "Estudiante");
            }
        }
    }
}

using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AltiFinReact.Core.Entities
{
    public class AppUser : IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser, int> manager, string authenticationType = DefaultAuthenticationTypes.ApplicationCookie)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class AppRole : IdentityRole<int, AppUserRole>
    {
        public AppRole() { }
        public AppRole(string name) { Name = name; }
    }
    public class AppUserRole : IdentityUserRole<int> { }
    public class AppUserClaim : IdentityUserClaim<int> { }
    public class AppUserLogin : IdentityUserLogin<int> { }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Oracon.Maps;
using Oracon.Models;
using System.Linq;
using System.Security.Claims;

namespace Oracon.Servicios
{
    public interface IClaimService
    {
        Usuario GetLoggedUser();
        void SetHttpContext(HttpContext httpContext);
        void Logout();
        void Login(ClaimsPrincipal principal);

    }

    public class ClaimService : IClaimService
    {
        private readonly Oracon_Context context;
        private HttpContext httpContext;

        public ClaimService(Oracon_Context context)
        {
            this.context = context;
        }

        public Usuario GetLoggedUser()
        {
            var claim = httpContext.User.Claims.FirstOrDefault();
            return context.Usuarios.Where(o => o.Username == claim.Value).FirstOrDefault();
        }

        public void Login(ClaimsPrincipal principal)
        {
            httpContext.SignInAsync(principal);
        }

        public void Logout()
        {
            httpContext.SignOutAsync();
        }

        public void SetHttpContext(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }
    }
}

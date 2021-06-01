using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Oracon.Models;
using Oracon.Maps;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Oracon.Servicios;
using Oracon.Repositorio;

namespace Oracon.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepo context;
        private readonly IClaimService claim;

        public HomeController(IHomeRepo context, IClaimService claim)
        {
            this.context = context;
            this.claim = claim;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                claim.SetHttpContext(HttpContext);
                var user = claim.GetLoggedUser();
                ViewBag.User = user;
            }
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                claim.SetHttpContext(HttpContext);
                var user = claim.GetLoggedUser();
                ViewBag.User = user;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //protected Usuario GetLoggedUser()
        //{
        //    var claim = HttpContext.User.Claims.FirstOrDefault();
        //    var user = context.Usuarios.Where(o => o.Username == claim.Value).FirstOrDefault();
        //    return user;
        //}
    }
}

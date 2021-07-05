using Microsoft.AspNetCore.Mvc;
using Oracon.Models;
using System.Diagnostics;
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
            ViewBag.Categorias = context.GetCategorias();
            ViewBag.Docentes = context.GetDocentes();
            ViewBag.Cursos = context.GetCursos();
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            ViewBag.Categorias = context.GetCategorias();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

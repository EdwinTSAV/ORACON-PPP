using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oracon.Repositorio;
using Oracon.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oracon.Controllers
{
    public class ApredizajeController : Controller
    {
        private readonly IAprendizajeRepo context;
        private readonly IClaimService claim;

        public ApredizajeController(IAprendizajeRepo context, IClaimService claim)
        {
            this.context = context;
            this.claim = claim;
        }
        [Authorize]
        public IActionResult Desarrollo()
        {
            ViewBag.Nombre = "Cursos";
            ViewBag.Categorias = context.GetCategorias();
            //var cursos = context.GetCursos();
            if (User.Identity.IsAuthenticated)
            {
                claim.SetHttpContext(HttpContext);
                //ViewBag.Favoritos = context.GetFavoritos(claim.GetLoggedUser().Id);
                //ViewBag.Compras = context.GetCursoUsuarios(claim.GetLoggedUser().Id);
            }
            return View();
        }
    }
}

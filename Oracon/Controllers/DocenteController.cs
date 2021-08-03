using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracon.Models;
using Oracon.Repositorio;
using Oracon.Servicios;
using System.Collections.Generic;
using System.Diagnostics;

namespace Oracon.Controllers
{
    [Authorize]
    public class DocenteController : Controller
    {
        private readonly IClaimService claim;
        private readonly IDocenteRepo context;

        public DocenteController(IDocenteRepo context, IClaimService claim)
        {
            this.context = context;
            this.claim = claim;
        }

        [HttpGet]
        public IActionResult Index()
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Categorias = context.GetCategorias();
                return View();
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult MisCursos(int idCategoria)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Categorias = context.GetCategorias();
                var cursos = context.GetCursosCategoriaDocente(idCategoria, claim.GetLoggedUser().Id);
                foreach (var item in ViewBag.Categorias)
                {
                    if (idCategoria == item.Id)
                        ViewBag.Nombre = item.Nombre;
                }
                if (ViewBag.Nombre == null)
                    return View("Error");
                return View(cursos);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult CrearCursos()
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Categorias = context.GetCategorias();
                return View(new Curso());
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public IActionResult CrearCursos(Curso curso, IFormFile image)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Categorias = context.GetCategorias();
                if (curso.Precio < 0)
                    ModelState.AddModelError("Precio", "El precio no puede ser negativo");
                if (ModelState.IsValid)
                {
                    context.SaveCurso(curso, claim.GetLoggedUser().Id, image);
                    return View("Index");
                }
                return View(curso);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult EditarCurso(int idCurso)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Categorias = context.GetCategorias();
                var curso = context.GetCursoIdUsuario(idCurso, claim.GetLoggedUser().Id);
                if (curso != null)
                    return View(curso);
                return View("Error");
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public IActionResult EditarCurso(Curso curso, IFormFile image)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Categorias = context.GetCategorias();
                if (ModelState.IsValid)
                {
                    context.UpdateCurso(curso, image);
                    return View("Index");
                }
                return View(curso);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public ActionResult EliminarCurso(int idCurso)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Categorias = context.GetCategorias();
                var curso = context.GetCursoIdUsuario(idCurso, claim.GetLoggedUser().Id);
                if (curso != null)
                {
                    context.DeleteCurso(curso);
                    return View("Index");
                }
                return View("Error");
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public ActionResult DetalleCurso()
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Cursos = context.GetCursosProceso(claim.GetLoggedUser().Id);
                ViewBag.Categorias = context.GetCategorias();
                return View();
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public ActionResult Aprendizaje()
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Cursos = context.GetCursosProceso(claim.GetLoggedUser().Id);
                ViewBag.Categorias = context.GetCategorias();
                return View();
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public ActionResult Aprendizaje(Aprendizaje aprendizaje)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Cursos = context.GetCursosProceso(claim.GetLoggedUser().Id);
                ViewBag.Categorias = context.GetCategorias();
                if (ModelState.IsValid)
                    context.SaveAprendizaje(aprendizaje);
                return View(aprendizaje);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public ActionResult Descripcion()
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Cursos = context.GetCursosProceso(claim.GetLoggedUser().Id);
                ViewBag.Categorias = context.GetCategorias();
                return View();
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public ActionResult Descripcion(int IdCurso, string Descripcion, string video)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Cursos = context.GetCursosProceso(claim.GetLoggedUser().Id);
                ViewBag.Categorias = context.GetCategorias();
                if (ModelState.IsValid)
                    context.ActualizaCurso(IdCurso, Descripcion, video);
                return View();
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public ActionResult Requisito()
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Cursos = context.GetCursosProceso(claim.GetLoggedUser().Id);
                ViewBag.Categorias = context.GetCategorias();
                return View();
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public ActionResult Requisito(Requisitos requisitos)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Cursos = context.GetCursosProceso(claim.GetLoggedUser().Id);
                ViewBag.Categorias = context.GetCategorias();
                if (ModelState.IsValid)
                    context.SaveRequisitos(requisitos);
                return View(requisitos);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public ActionResult Contenido()
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Cursos = context.GetCursosProceso(claim.GetLoggedUser().Id);
                ViewBag.Categorias = context.GetCategorias();
                return View();
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public ActionResult Contenido(int IdCurso, int IdModulo, string NombreModulo, string NombreClase, string videoClase)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Cursos = context.GetCursosProceso(claim.GetLoggedUser().Id);
                ViewBag.Categorias = context.GetCategorias();
                if (IdCurso == 0)
                    ModelState.AddModelError("IdCurso", "Elige un curso");
                else if(IdModulo == 0)
                {
                    Modulo modulo = new Modulo() {
                        IdCurso = IdCurso,
                        Nombre = NombreModulo,
                        Clases = new List<Clases>
                        {
                            new Clases
                            {
                                Nombre = NombreClase,
                                Video = videoClase
                            }
                        }
                    };
                    context.SaveModulos(modulo);
                }
                else if (IdModulo > 0)
                {
                    var modulo = context.GetModulo(IdModulo);
                    modulo.Clases = new List<Clases>
                    {
                        new Clases
                        {
                            Nombre = NombreClase,
                            Video = videoClase
                        }
                    };
                    context.UpdateModulos(modulo);
                }
                return View();
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult Modulos(int idCurso)
        {
            var modulos = context.GetModulos(idCurso);
            return Json(modulos);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                ViewBag.Categorias = context.GetCategorias();
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            else
                return RedirectToAction("Error", "Home");
        }
    }
}

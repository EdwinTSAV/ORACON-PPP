using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracon.Maps;
using Oracon.Models;
using Oracon.Servicios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Oracon.Controllers
{
    [Authorize]
    public class DocenteController : Controller
    {
        private readonly IClaimService claim;
        private readonly Oracon_Context context;
        private readonly IConfiguration configuration;
        public readonly IWebHostEnvironment hosting;

        public DocenteController(Oracon_Context context, IClaimService claim, IConfiguration configuration, IWebHostEnvironment hosting)
        {
            this.context = context;
            this.claim = claim;
            this.configuration = configuration;
            this.hosting = hosting;
        }

        [HttpGet]
        public IActionResult Index()
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                var categorias = context.Categorias.ToList();
                ViewBag.Categorias = categorias;
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
                var categorias = context.Categorias.ToList();
                ViewBag.Categorias = categorias;
                var cursos = context.Cursos.
                    Where(o => o.IdCategoria == idCategoria && o.IdDocente == claim.GetLoggedUser().Id).
                    Include(o => o.Categoria).
                    ToList();
                foreach (var item in categorias)
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
                var categorias = context.Categorias.ToList();
                ViewBag.Categorias = categorias;
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
                curso.IdDocente = claim.GetLoggedUser().Id;
                if (ModelState.IsValid)
                {
                    if (image != null)
                        curso.Imagen = SaveFile(image);
                    else
                        curso.Imagen = "/project/icono.jpg";
                    context.Cursos.Add(curso);
                    context.SaveChanges();
                    return View("Index");
                }
                ViewBag.Categorias = context.Categorias.ToList();
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
                var categorias = context.Categorias.ToList();
                ViewBag.Categorias = categorias;
                var curso = context.Cursos.
                    Where(o => o.Id == idCurso && o.IdDocente == claim.GetLoggedUser().Id).
                    FirstOrDefault();
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
                if (ModelState.IsValid)
                {
                    if (image != null)
                        curso.Imagen = SaveFile(image);
                    else
                        curso.Imagen = "/project/icono.jpg";
                    context.Cursos.Update(curso);
                    context.SaveChanges();
                    return View("Index");
                }
                ViewBag.Categorias = context.Categorias.ToList();
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
                var categorias = context.Categorias.ToList();
                ViewBag.Categorias = categorias;
                var cursos = context.Cursos.
                    Where(o => o.Id == idCurso && o.IdDocente == claim.GetLoggedUser().Id).
                    FirstOrDefault();
                if (cursos != null)
                {
                    context.Cursos.Remove(cursos);
                    context.SaveChanges();
                    return View("Index");
                }
                return View("Error");
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 2)
            {
                var categorias = context.Categorias.ToList();
                ViewBag.Categorias = categorias;
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            else
                return RedirectToAction("Error", "Home");
        }

        protected string SaveFile(IFormFile file)
        {
            string relativePath = "";

            if (file.Length > 0 && (file.ContentType == "image/jpeg" || file.ContentType == "image/png" || file.ContentType == "image/gif"))
            {
                relativePath = Path.Combine("FilesBD", file.FileName);
                var filePath = Path.Combine(hosting.WebRootPath, relativePath);
                var stream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(stream);
                stream.Close();
            }

            return "/" + relativePath.Replace('\\', '/');
        }
    }
}

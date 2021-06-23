using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracon.Maps;
using Oracon.Models;
using Oracon.Servicios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Oracon.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IClaimService claim;
        private readonly Oracon_Context context;
        private readonly IConfiguration configuration;

        public AdminController(Oracon_Context context, IClaimService claim, IConfiguration configuration)
        {
            this.context = context;
            this.claim = claim;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                ViewBag.Categorias = context.Categorias.ToList();
                return View();
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult Usuarios(int idRol)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                ViewBag.Categorias = context.Categorias.ToList();
                var usuario = context.Usuarios.
                    Where(o => o.IdRol == idRol).
                    Include(o => o.Roles).
                    ToList();
                if (idRol == 2)
                    ViewBag.Nombre = "Docentes";
                else if (idRol == 3)
                    ViewBag.Nombre = "Estudiantes";
                else
                    return View("Error");
                return View(usuario);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult CrearUsuarios()
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                ViewBag.Categorias = context.Categorias.ToList();
                ViewBag.Roles = context.Roles.ToList();
                return View(new Usuario());
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public IActionResult CrearUsuarios(Usuario usuario, string passwordConf)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                if (usuario.Password != passwordConf)
                    ModelState.AddModelError("Password", "Las contraseñas no coinciden");
                var usuarios = context.Usuarios.ToList();
                foreach (var item in usuarios)
                {
                    if (item.Correo == usuario.Correo)
                        ModelState.AddModelError("Correo", "Este correo ya se encuentra registrado");
                    if (item.Username == usuario.Username)
                        ModelState.AddModelError("Username", "Este usuario ya existe, intenta otro");
                }

                if (ModelState.IsValid)
                {
                    usuario.Imagen = "/project/userperfil.png";
                    usuario.Password = CreateHash(usuario.Password);
                    context.Usuarios.Add(usuario);
                    context.SaveChanges();
                    return View("Index");
                }
                ViewBag.Categorias = context.Categorias.ToList();
                ViewBag.Roles = context.Roles.ToList();
                return View(usuario);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult EditarUsuario(int idUser)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                ViewBag.Categorias = context.Categorias.ToList();
                ViewBag.Roles = context.Roles.ToList();
                var usuarios = context.Usuarios.Where(o => o.Id == idUser).FirstOrDefault();
                if (usuarios != null)
                    return View(usuarios);
                return View("Error");
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public IActionResult EditarUsuario(Usuario usuario)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                var usuarios = context.Usuarios.Where(o => o.Id != usuario.Id).ToList();
                foreach (var item in usuarios)
                {
                    if (item.Correo == usuario.Correo)
                        ModelState.AddModelError("Correo", "Este correo ya se encuentra registrado");
                    if (item.Username == usuario.Username)
                        ModelState.AddModelError("Username", "Este usuario ya existe, intenta otro");
                }

                if (ModelState.IsValid)
                {
                    context.Usuarios.Update(usuario);
                    context.SaveChanges();
                    return View("Index");
                }
                ViewBag.Categorias = context.Categorias.ToList();
                ViewBag.Roles = context.Roles.ToList();
                return View(usuario);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public ActionResult EliminarUsuario(int idUser)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                var usuarios = context.Usuarios.Where(o => o.Id == idUser).FirstOrDefault();
                if (usuarios != null)
                {
                    context.Usuarios.Remove(usuarios);
                    context.SaveChanges();
                    return View("Index");
                }
                ViewBag.Categorias = context.Categorias.ToList();
                return View("Error");
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult Cursos(int idCategoria)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                var cursos = context.Cursos.
                    Where(o => o.IdCategoria == idCategoria).
                    Include(o => o.Categoria).
                    Include(o => o.Docente).
                    ToList();
                ViewBag.Categorias = context.Categorias.ToList();
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
            if (claim.GetLoggedUser().IdRol == 1)
            {
                ViewBag.Docentes = context.Usuarios.Where(o => o.IdRol == 2).ToList();
                ViewBag.Categorias = context.Categorias.ToList();
                return View(new Curso());
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public IActionResult CrearCursos(Curso curso)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                if (ModelState.IsValid)
                {
                    curso.Imagen = "/project/icono.jpg";
                    context.Cursos.Add(curso);
                    context.SaveChanges();
                    return View("Index");
                }
                ViewBag.Docentes = context.Usuarios.Where(o => o.IdRol == 2).ToList();
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
            if (claim.GetLoggedUser().IdRol == 1)
            {
                var categorias = context.Categorias.ToList();
                ViewBag.Docentes = context.Usuarios.Where(o => o.IdRol == 2).ToList();
                ViewBag.Categorias = categorias;
                var curso = context.Cursos.Where(o => o.Id == idCurso).FirstOrDefault();
                if (curso != null)
                    return View(curso);
                return View("Error");
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public IActionResult EditarCurso(Curso curso)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                if (ModelState.IsValid)
                {
                    context.Cursos.Update(curso);
                    context.SaveChanges();
                    return View("Index");
                }
                ViewBag.Docentes = context.Usuarios.Where(o => o.IdRol == 2).ToList();
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
            if (claim.GetLoggedUser().IdRol == 1)
            {
                var categorias = context.Categorias.ToList();
                ViewBag.Categorias = categorias;
                var cursos = context.Cursos.Where(o => o.Id == idCurso).FirstOrDefault();
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
            if (claim.GetLoggedUser().IdRol == 1) 
            {
                var categorias = context.Categorias.ToList();
                ViewBag.Categorias = categorias;
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            else
                return RedirectToAction("Error", "Home");
        }

        protected string CreateHash(string input)
        {
            input += configuration.GetValue<string>("Token");
            var sha = SHA512.Create();
            var bytes = Encoding.Default.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oracon.Models;
using Oracon.Repositorio;
using Oracon.Servicios;
using System.Diagnostics;

namespace Oracon.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IClaimService claim;
        private readonly IAdminRepo context;

        public AdminController(IAdminRepo context, IClaimService claim)
        {
            this.context = context;
            this.claim = claim;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Categorias = context.GetCategorias();
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                var compras = context.GetCompras();
                return View(compras);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult Usuarios(int idRol)
        {
            ViewBag.Categorias = context.GetCategorias();
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                var usuario = context.GetUsuariosRol(idRol);
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
            ViewBag.Categorias = context.GetCategorias();
            ViewBag.Roles = context.GetRoles();
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                return View(new Usuario());
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public IActionResult CrearUsuarios(Usuario usuario, string passwordConf)
        {
            ViewBag.Categorias = context.GetCategorias();
            ViewBag.Roles = context.GetRoles();
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                if (usuario.Password != passwordConf)
                    ModelState.AddModelError("Password", "Las contraseñas no coinciden");
                var usuarios = context.GetUsuarios();
                foreach (var item in usuarios)
                {
                    if (item.Correo == usuario.Correo)
                        ModelState.AddModelError("Correo", "Este correo ya se encuentra registrado");
                    if (item.Username == usuario.Username)
                        ModelState.AddModelError("Username", "Este usuario ya existe, intenta otro");
                }

                if (ModelState.IsValid)
                {
                    context.SaveUser(usuario);
                    return View("Index");
                }
                return View(usuario);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult EditarUsuario(int idUser)
        {
            ViewBag.Categorias = context.GetCategorias();
            ViewBag.Roles = context.GetRoles();
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                var usuarios = context.GetUsuarioId(idUser);
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
            ViewBag.Categorias = context.GetCategorias();
            ViewBag.Roles = context.GetRoles();
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                var usuarios = context.GetUsuariosIdNo(usuario.Id);
                foreach (var item in usuarios)
                {
                    if (item.Correo == usuario.Correo)
                        ModelState.AddModelError("Correo", "Este correo ya se encuentra registrado");
                    if (item.Username == usuario.Username)
                        ModelState.AddModelError("Username", "Este usuario ya existe, intenta otro");
                }

                if (ModelState.IsValid)
                {
                    context.UpdateUser(usuario);
                    return View("Index");
                }
                return View(usuario);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public ActionResult EliminarUsuario(int idUsuario)
        {
            ViewBag.Categorias = context.GetCategorias();
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                var usuarios = context.GetUsuarioId(idUsuario);
                if (usuarios != null)
                {
                    context.DeleteUser(usuarios);
                    return View("Index");
                }
                return View("Error");
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult Cursos(int idCategoria)
        {
            ViewBag.Categorias = context.GetCategorias();
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                var cursos = context.GetCursos(idCategoria);
                
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
            ViewBag.Categorias = context.GetCategorias();
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                ViewBag.Docentes = context.GetUsuariosRol(2);
                return View(new Curso());
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public IActionResult CrearCursos(Curso curso)
        {
            ViewBag.Categorias = context.GetCategorias();
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                if (ModelState.IsValid)
                {
                    context.SaveCurso(curso);
                    return View("Index");
                }
                ViewBag.Docentes = context.GetUsuariosRol(2);
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
                ViewBag.Docentes = context.GetUsuariosRol(2);
                ViewBag.Categorias = context.GetCategorias();
                var curso = context.GetCurso(idCurso);
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
                ViewBag.Docentes = context.GetUsuariosRol(2);
                ViewBag.Categorias = context.GetCategorias();
                if (ModelState.IsValid)
                {
                    context.UpdateCurso(curso);
                    return View("Index");
                }
                return View(curso);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult EliminarCurso(int idCurso)
        {
            ViewBag.Categorias = context.GetCategorias();
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                var cursos = context.GetCurso(idCurso);
                if (cursos != null)
                {
                    context.DeleteCurso(cursos);
                    return View("Index");
                }
                return View("Error");
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult Compras()
        {
            ViewBag.Categorias = context.GetCategorias();
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                var compras = context.GetCompras();
                return View(compras);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public void Comprado(int idCompra)
        {
            ViewBag.Categorias = context.GetCategorias();
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1)
            {
                context.AceptaCompra(idCompra);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.Categorias = context.GetCategorias();
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().IdRol == 1) 
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            else
                return RedirectToAction("Error", "Home");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Oracon.Repositorio;
using Oracon.Servicios;

namespace Oracon.Controllers
{
    public class CursoController : Controller
    {
        private readonly ICursoRepo context;
        private readonly IClaimService claim;
        public CursoController(ICursoRepo context, IClaimService claim)
        {
            this.context = context;
            this.claim = claim;
        }

        public IActionResult Cursos(int idCategoria, int idDocente)
        {
            ViewBag.Nombre = "Cursos";
            ViewBag.Categorias = context.GetCategorias();
            var cursos = context.GetCursos();
            if (idDocente > 0)
            {
                var docente = context.GetDocente(idDocente);
                cursos = context.GetCursoDocentes(idDocente);
                ViewBag.Nombre = docente.Nombres;
                if (docente == null)
                    RedirectToAction("Error", "Home");
            }
            else if (idCategoria > 0)
            {
                foreach (var item in ViewBag.Categorias)
                {
                    if (item.Id == idCategoria)
                    {
                        cursos = context.GetCursosCategoria(idCategoria);
                        ViewBag.Nombre = item.Nombre;
                    }
                }
            }
            if (User.Identity.IsAuthenticated)
            {
                claim.SetHttpContext(HttpContext);
                ViewBag.Favoritos = context.GetFavoritos(claim.GetLoggedUser().Id);
                ViewBag.Compras = context.GetCursoUsuarios(claim.GetLoggedUser().Id);
            }
            return View(cursos);
        }

        [HttpGet]
        public IActionResult Detalle(int idCurso)
        {
            ViewBag.Categorias = context.GetCategorias();
            var curso = context.GetCurso(idCurso);
            if (User.Identity.IsAuthenticated)
            {
                claim.SetHttpContext(HttpContext);
                ViewBag.Favoritos = context.GetFavoritos(claim.GetLoggedUser().Id);
                ViewBag.Compras = context.GetCursoUsuarios(claim.GetLoggedUser().Id);
            }
            ViewBag.Nombre = curso.Nombre;
            return View(curso);
        }

        [HttpPost]
        public IActionResult Comentario(int idCurso, string Comentario)
        {
            ViewBag.Categorias = context.GetCategorias();
            var curso = context.GetCurso(idCurso);
            if (User.Identity.IsAuthenticated)
            {
                claim.SetHttpContext(HttpContext);
                ViewBag.Favoritos = context.GetFavoritos(claim.GetLoggedUser().Id);
                ViewBag.Compras = context.GetCursoUsuarios(claim.GetLoggedUser().Id);
                if (Comentario != null)
                {
                    context.SaveComentario(claim.GetLoggedUser().Id, idCurso, Comentario);
                }
            }
            ViewBag.Nombre = curso.Nombre;
            return View("Detalle", curso);
        }

        public IActionResult Docentes()
        {
            ViewBag.Categorias = context.GetCategorias();
            var docentes = context.GetDocentes();
            return View(docentes);
        }

        public IActionResult Aprendizaje()
        {
            ViewBag.Categorias = context.GetCategorias();
            claim.SetHttpContext(HttpContext);
            var cursos = context.GetCursoUsuariosPagado(claim.GetLoggedUser().Id);
            return View(cursos);
        }

        public IActionResult Favorito()
        {
            ViewBag.Categorias = context.GetCategorias();
            claim.SetHttpContext(HttpContext);
            var cursos = context.GetFavoritos(claim.GetLoggedUser().Id);
            return View(cursos);
        }

        public IActionResult Carrito()
        {
            ViewBag.Categorias = context.GetCategorias();
            claim.SetHttpContext(HttpContext);
            var cursos = context.GetCursoUsuariosNoPagado(claim.GetLoggedUser().Id);
            return View(cursos);
        }

        [HttpGet]
        public void Favoritos(int idCurso)
        {
            claim.SetHttpContext(HttpContext);
            var favorito = context.GetFavorito(claim.GetLoggedUser().Id, idCurso);
            if (favorito != null)
            {
                context.DeleteFavorito(favorito);
                ModelState.AddModelError("Elimina","Curso eliminado de favoritos");
            }
            if (ModelState.IsValid)
                context.SaveFavorito(idCurso, claim.GetLoggedUser().Id);
        }

        [HttpGet]
        public void Compras(int idCurso)
        {
            claim.SetHttpContext(HttpContext);
            var compras = context.GetCompra(claim.GetLoggedUser().Id, idCurso);
            var curso = context.GetCurso(idCurso);
            if (compras != null)
            {
                context.DeleteCursoUsuario(compras);
                ModelState.AddModelError("Existe", "Ya Comprado");
            }
            if (ModelState.IsValid)
                if (curso.Precio > 0)
                    context.SaveCursoUsuario(idCurso, claim.GetLoggedUser().Id);
                else
                    context.SaveCursoUsuarioGratuito(idCurso, claim.GetLoggedUser().Id);
        }
    }
}

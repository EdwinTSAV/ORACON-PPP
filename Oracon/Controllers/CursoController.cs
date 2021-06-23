using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracon.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oracon.Controllers
{
    public class CursoController : Controller
    {
        private readonly Oracon_Context context;

        public CursoController(Oracon_Context context)
        {
            this.context = context;
        }

        public IActionResult Categorias(int idCategoria)
        {
            ViewBag.Categorias = context.Categorias.ToList();

            var cursos = context.Cursos.
                Include(o => o.Docente).
                Include(o => o.Categoria).
                ToList();
            foreach (var item in ViewBag.Categorias)
            {
                if (item.Id == idCategoria)
                {
                    cursos = cursos.
                        Where(o => o.IdCategoria == idCategoria).
                        ToList();
                }
            }
            return View(cursos);
        }

        public IActionResult Docentes(int idCategoria)
        {
            ViewBag.Categorias = context.Categorias.ToList();
            var docentes = context.Usuarios.Where(o => o.IdRol == 2).ToList();
            return View(docentes);
        }
    }
}

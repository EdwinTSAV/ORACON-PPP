using Oracon.Maps;
using Oracon.Models;
using System.Collections.Generic;
using System.Linq;

namespace Oracon.Repositorio
{
    public interface IHomeRepo
    {
        List<Categoria> GetCategorias();
        List<Usuario> GetDocentes();
        List<Curso> GetCursos();
    }
    public class HomeRepo : IHomeRepo
    {
        private readonly Oracon_Context context;

        public HomeRepo(Oracon_Context context)
        {
            this.context = context;
        }

        public List<Categoria> GetCategorias()
        {
            return context.Categorias.ToList();
        }

        public List<Curso> GetCursos()
        {
            return context.Cursos.ToList();
        }

        public List<Usuario> GetDocentes()
        {
            return context.Usuarios.Where(o => o.IdRol == 2).ToList();
        }
    }
}

using Oracon.Maps;
using Oracon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oracon.Repositorio
{
    public interface IHomeRepo
    {
        List<Categoria> GetCategorias();
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
    }
}

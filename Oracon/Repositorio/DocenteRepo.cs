using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Oracon.Maps;
using Oracon.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Oracon.Repositorio
{
    public interface IDocenteRepo
    {
        List<Categoria> GetCategorias();
        List<Curso> GetCursosCategoriaDocente(int idCategoria, int idUser);
        Curso GetCursoIdUsuario(int idCurso, int idDocente);
        void SaveCurso(Curso curso, int idUser, IFormFile image);
        void UpdateCurso(Curso curso, IFormFile image);
        void DeleteCurso(Curso curso);
        List<Curso> GetCursosProceso(int idUser);
        List<Aprendizaje> GetAprendizajes(int IdCurso);
        void SaveAprendizaje(Aprendizaje aprendizaje);
    }
    public class DocenteRepo : IDocenteRepo
    {
        private readonly Oracon_Context context;
        public readonly IWebHostEnvironment hosting;

        public DocenteRepo(Oracon_Context context, IWebHostEnvironment hosting)
        {
            this.context = context;
            this.hosting = hosting;
        }
        public List<Categoria> GetCategorias()
        {
            return context.Categorias.ToList();
        }

        public List<Curso> GetCursosCategoriaDocente(int idCategoria, int idUser)
        {
            return context.Cursos.
                    Where(o => o.IdCategoria == idCategoria && o.IdDocente == idUser).
                    Include(o => o.Categoria).
                    ToList();
        }

        public void SaveCurso(Curso curso, int idUser, IFormFile image)
        {
            if (image != null)
                curso.Imagen = SaveFile(image);
            else
                curso.Imagen = "/project/icono.jpg";
            curso.IdDocente = idUser;
            context.Cursos.Add(curso);
            context.SaveChanges();
        }

        public Curso GetCursoIdUsuario(int idCurso, int idDocente)
        {
            return context.Cursos.
                    Where(o => o.Id == idCurso && o.IdDocente == idDocente).
                    FirstOrDefault();
        }

        public void UpdateCurso(Curso curso, IFormFile image)
        {
            if (image != null)
                curso.Imagen = SaveFile(image);
            context.Cursos.Update(curso);
            context.SaveChanges();
        }

        public void DeleteCurso(Curso curso)
        {
            context.Cursos.Remove(curso);
            context.SaveChanges();
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

        public List<Curso> GetCursosProceso(int idUser)
        {
            return context.Cursos.
                    Where(o => !o.Estado && o.IdDocente == idUser).
                    Include(o => o.Categoria).
                    ToList();
        }

        public void SaveAprendizaje(Aprendizaje aprendizaje)
        {
            context.Aprendizajes.Add(aprendizaje);
            context.SaveChanges();
        }

        public List<Aprendizaje> GetAprendizajes(int IdCurso)
        {
            return context.Aprendizajes.Where(o => o.IdCurso == IdCurso).ToList();
        }
    }
}

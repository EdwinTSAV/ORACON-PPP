using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracon.Maps;
using Oracon.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Oracon.Repositorio
{
    public interface IAjustesRepo
    {
        List<Usuario> GetUsuarios(int idUser);
        Usuario GetUsuario(int idUser);
        void UpdateUser(Usuario usuario, IFormFile image);
        List<Favoritos> GetFavoritos(int idUser);
        List<CursoUsuario> GetCursoUsuarios(int idUser);
        List<CursoUsuario> GetCursoUsuariosT(int idUser);
    }
    public class AjustesRepo : IAjustesRepo
    {
        private readonly Oracon_Context context;
        public readonly IWebHostEnvironment hosting;

        public AjustesRepo(Oracon_Context context, IWebHostEnvironment hosting)
        {
            this.context = context;
            this.hosting = hosting;
        }

        public List<Usuario> GetUsuarios(int idUser)
        {
            return context.Usuarios.Where(o => o.Id != idUser).ToList();
        }

        public Usuario GetUsuario(int idUser)
        {
            return context.Usuarios.Where(o => o.Id == idUser).FirstOrDefault();
        }

        public void UpdateUser(Usuario usuario, IFormFile image)
        {
            if (image != null)
                usuario.Imagen = SaveFile(image);
            context.Usuarios.Update(usuario);
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

        public List<Favoritos> GetFavoritos(int idUser)
        {
            return context.Favoritos.
                Include(o => o.Cursos).
                Include(o => o.Usuarios).
                Include(o => o.Cursos.Docente).
                Where(o => o.IdUser == idUser).
                ToList();
        }

        public List<CursoUsuario> GetCursoUsuarios(int idUser)
        {
            return context.CursoUsuario.
                Include(o => o.Cursos).
                Include(o => o.Usuarios).
                Include(o => o.Cursos.Docente).
                Where(o => o.IdUser == idUser).
                ToList();
        }

        public List<CursoUsuario> GetCursoUsuariosT(int idUser)
        {
            return context.CursoUsuario.
                Include(o => o.Cursos).
                Include(o => o.Usuarios).
                Include(o => o.Cursos.Docente).
                Where(o => o.IdUser == idUser && o.Estado).
                ToList();
        }
    }
}

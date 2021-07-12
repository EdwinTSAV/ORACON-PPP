using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracon.Maps;
using Oracon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Oracon.Repositorio
{
    public interface IAdminRepo
    {
        List<Categoria> GetCategorias();
        List<Roles> GetRoles();
        List<Usuario> GetUsuarios();
        List<Usuario> GetUsuariosRol(int idRol);
        List<Usuario> GetUsuariosIdNo(int id);
        Usuario GetUsuarioId(int idUser);
        void SaveUser(Usuario usuario);
        void UpdateUser(Usuario usuario);
        void DeleteUser(Usuario usuario);
        List<Curso> GetCursos(int idCategoria);
        Curso GetCurso(int idCurso);
        void SaveCurso(Curso curso);
        void UpdateCurso(Curso curso);
        void DeleteCurso(Curso curso);
        List<CursoUsuario> GetCompras();
        void AceptaCompra(int idCompra);
    }
    public class AdminRepo : IAdminRepo
    {
        private readonly Oracon_Context context;
        private readonly IConfiguration configuration;

        public AdminRepo(Oracon_Context context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public Curso GetCurso(int idCurso)
        {
            return context.Cursos.Where(o => o.Id == idCurso).FirstOrDefault();
        }

        public void DeleteCurso(Curso curso)
        {
            context.Cursos.Remove(curso);
            context.SaveChanges();
        }

        public void DeleteUser(Usuario usuario)
        {
            context.Usuarios.Remove(usuario);
            context.SaveChanges();
        }

        public List<Categoria> GetCategorias()
        {
            return context.Categorias.ToList();
        }

        public List<Curso> GetCursos(int idCategoria)
        {
            return context.Cursos.
                Where(o => o.IdCategoria == idCategoria).
                Include(o => o.Categoria).
                Include(o => o.Docente).
                ToList();
        }

        public List<Roles> GetRoles()
        {
            return context.Roles.ToList();
        }

        public Usuario GetUsuarioId(int idUser)
        {
            return context.Usuarios.Where(o => o.Id == idUser).FirstOrDefault();
        }

        public List<Usuario> GetUsuarios()
        {
            return context.Usuarios.ToList();
        }

        public List<Usuario> GetUsuariosIdNo(int id)
        {
            return context.Usuarios.Where(o => o.Id != id).ToList();
        }

        public List<Usuario> GetUsuariosRol(int idRol)
        {
            return context.Usuarios.
                    Where(o => o.IdRol == idRol).
                    Include(o => o.Roles).
                    ToList();
        }

        public void SaveCurso(Curso curso)
        {
            curso.Estado = false;
            curso.Imagen = "/project/icono.jpg";
            context.Cursos.Add(curso);
            context.SaveChanges();
        }

        public void SaveUser(Usuario usuario)
        {
            usuario.Imagen = "/project/userperfil.png";
            usuario.Password = CreateHash(usuario.Password);
            context.Usuarios.Add(usuario);
            context.SaveChanges();
        }

        public void UpdateCurso(Curso curso)
        {
            context.Cursos.Update(curso);
            context.SaveChanges();
        }

        public void UpdateUser(Usuario usuario)
        {
            context.Usuarios.Update(usuario);
            context.SaveChanges();
        }
        protected string CreateHash(string input)
        {
            input += configuration.GetValue<string>("Token");
            var sha = SHA512.Create();
            var bytes = Encoding.Default.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public List<CursoUsuario> GetCompras()
        {
            return context.CursoUsuario
                .Where(o => !o.Pagado)
                .Include(o => o.Cursos)
                .Include(o => o.Cursos.Docente)
                .Include(o => o.Usuarios)
                .ToList();
        }

        public void AceptaCompra(int idCompra)
        {
            var comprado = context.CursoUsuario
                .Where(o => o.Id == idCompra)
                .FirstOrDefault();
            comprado.Pagado = true;
            context.CursoUsuario.Update(comprado);
            context.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Oracon.Maps;
using Oracon.Models;
using System.Collections.Generic;
using System.Linq;

namespace Oracon.Repositorio
{
    public interface ICursoRepo
    {
        List<Categoria> GetCategorias();
        List<Curso> GetCursos();
        Curso GetCurso(int idCurso);
        List<Curso> GetCursosCategoria(int idCategoria);
        List<Usuario> GetDocentes();
        List<Curso> GetCursoDocentes(int idDocente);
        Usuario GetDocente(int idDocente);
        List<Favoritos> GetFavoritos(int idUser);
        Favoritos GetFavorito(int idUser, int idCurso);
        void SaveFavorito(int idCurso, int idUser);
        void DeleteFavorito(Favoritos favoritos);
        List<CursoUsuario> GetCursoUsuarios(int idUser);
        List<CursoUsuario> GetCursoUsuariosPagado(int idUser);
        List<CursoUsuario> GetCursoUsuariosNoPagado(int idUser);
        CursoUsuario GetCompra(int idUser, int idCurso);
        void SaveCursoUsuario(int idCurso, int idUser);
        void SaveCursoUsuarioGratuito(int idCurso, int idUser);
        void SaveComentario(int idUsuario, int idCurso, string Comentario);
        void DeleteCursoUsuario(CursoUsuario cursoUsuario);
    }
    public class CursoRepo : ICursoRepo
    {
        private readonly Oracon_Context context;
        public CursoRepo(Oracon_Context context)
        {
            this.context = context;
        }

        public List<Categoria> GetCategorias()
        {
            return context.Categorias.ToList();
        }

        public List<Curso> GetCursoDocentes(int idDocente)
        {
            return context.Cursos.Where(o => o.IdDocente == idDocente)
                .Include(o => o.Docente)
                .Include(o => o.Categoria)
                .ToList();
        }

        public List<Curso> GetCursos()
        {
            return context.Cursos.
                Include(o => o.Docente).
                Include(o => o.Categoria).
                ToList();
        }

        public List<Curso> GetCursosCategoria(int idCategoria)
        {
            return context.Cursos.
                Where(o => o.IdCategoria == idCategoria).
                ToList();
        }

        public List<Usuario> GetDocentes()
        {
            return context.Usuarios.Where(o => o.IdRol == 2).ToList();
        }

        public Usuario GetDocente(int idDocente)
        {
            return context.Usuarios.Where(o => o.IdRol == 2 && o.Id == idDocente).FirstOrDefault();
        }

        public void SaveFavorito(int idCurso, int idUser)
        {
            Favoritos favoritos = new Favoritos
            {
                IdCurso = idCurso,
                IdUser = idUser
            };
            context.Add(favoritos);
            context.SaveChanges();
        }

        public void DeleteFavorito(Favoritos favoritos)
        {
            context.Remove(favoritos);
            context.SaveChanges();
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

        public Favoritos GetFavorito(int idUser, int idCurso)
        {
            return context.Favoritos.Where(o => o.IdUser == idUser && o.IdCurso == idCurso).FirstOrDefault();
        }

        public List<CursoUsuario> GetCursoUsuarios(int idUser)
        {
            return context.CursoUsuario.
                Include(o => o.Cursos).
                Include(o => o.Usuarios).
                Include(o => o.Cursos.Docente).
                Include(o => o.Cursos.Categoria).
                Where(o => o.IdUser == idUser).
                ToList();
        }

        public void SaveCursoUsuario(int idCurso, int idUser)
        {
            CursoUsuario compra = new CursoUsuario
            {
                IdCurso = idCurso,
                IdUser = idUser,
                Pagado = false
            };
            context.Add(compra);
            context.SaveChanges();
        }

        public void SaveCursoUsuarioGratuito(int idCurso, int idUser)
        {
            CursoUsuario compra = new CursoUsuario
            {
                IdCurso = idCurso,
                IdUser = idUser,
                Pagado = true
            };
            context.Add(compra);
            context.SaveChanges();
        }

        public CursoUsuario GetCompra(int idUser, int idCurso)
        {
            return context.CursoUsuario.Where(o => o.IdUser == idUser && o.IdCurso == idCurso).FirstOrDefault();
        }

        public Curso GetCurso(int idCurso)
        {
            context.Modulos.Include(o => o.Clases).ToList();
            return context.Cursos.
                Where(o => o.Id == idCurso).
                Include(o => o.Docente).
                Include(o => o.Aprendizajes).
                Include(o => o.Requisitos).
                Include(o => o.Modulos).
                Include(o => o.Comentarios).
                FirstOrDefault();
        }

        public void DeleteCursoUsuario(CursoUsuario cursoUsuario)
        {
            context.Remove(cursoUsuario);
            context.SaveChanges();
        }

        public void SaveComentario(int idUsuario, int idCurso, string Comentario)
        {
            ComentarioCurso comentario = new ComentarioCurso();
            comentario.IdCurso = idCurso;
            comentario.IdUsuario = idUsuario;
            comentario.Comentario = Comentario;
            context.Add(comentario);
            context.SaveChanges();
        }

        public List<CursoUsuario> GetCursoUsuariosPagado(int idUser)
        {
            return context.CursoUsuario.
                Include(o => o.Cursos).
                Include(o => o.Usuarios).
                Include(o => o.Cursos.Docente).
                Include(o => o.Cursos.Categoria).
                Where(o => o.IdUser == idUser && o.Pagado).
                ToList();
        }

        public List<CursoUsuario> GetCursoUsuariosNoPagado(int idUser)
        {
            return context.CursoUsuario.
                Include(o => o.Cursos).
                Include(o => o.Usuarios).
                Include(o => o.Cursos.Docente).
                Include(o => o.Cursos.Categoria).
                Where(o => o.IdUser == idUser && !o.Pagado).
                ToList();
        }
    }
}

namespace Oracon.Models
{
    public class Favoritos
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdCurso { get; set; }

        public Curso Cursos { get; set; }
        public Usuario Usuarios { get; set; }
    }
}

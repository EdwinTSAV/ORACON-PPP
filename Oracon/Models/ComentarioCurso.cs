using System.ComponentModel.DataAnnotations;

namespace Oracon.Models
{
    public class ComentarioCurso
    {
        public int Id { get; set; }
        public int IdCurso { get; set; }
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Comentario { get; set; }
        public Usuario Usuario { get; set; }
    }
}

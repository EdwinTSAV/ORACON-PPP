using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oracon.Models
{
    public class Curso
    {
        public int Id { get; set; }

        public string Imagen { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "Debe seleccionar uno")]
        public int IdDocente { get; set; }

        [Required(ErrorMessage = "Debe seleccionar uno")]
        public int IdCategoria { get; set; }
        public bool Estado { get; set; }

        public Usuario Docente { get; set; }
        public Categoria Categoria { get; set; }
        public List<Aprendizaje> Aprendizajes { get; set; }
    }
}

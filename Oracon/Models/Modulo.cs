using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oracon.Models
{
    public class Modulo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdCurso { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }
        public List<Clases> Clases { get; set; }
    }
}

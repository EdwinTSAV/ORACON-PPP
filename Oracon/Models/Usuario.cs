using System.ComponentModel.DataAnnotations;

namespace Oracon.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        
        public string Imagen { get; set; }
        
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombres { get; set; }
        
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Apellidos { get; set; }
        
        public string Celular { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [EmailAddress(ErrorMessage = "El campo no es una dirección de correo electrónico válida")]
        public string Correo { get; set; }
                
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@"[a-zA-Z0-9]+" , ErrorMessage = "Carácteres permitidos (a-z A-Z 0-9)")]
        [MinLength(5, ErrorMessage = "Usuario minimo 5 caracteres")]
        [MaxLength(10, ErrorMessage = "Usuario máximo 10 caracteres")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Contraseña minimo 6 caracteres")]
        [MaxLength(25, ErrorMessage = "Contraseña máximo 25 caracteres")]
        public string Password { get; set; }

        public string Recovery { get; set; }

        public int IdRol { get; set; }

        public Roles Roles { get; set; }
    }
}

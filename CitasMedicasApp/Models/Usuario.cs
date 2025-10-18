using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitasMedicasApp.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Contrasena { get; set; }

        [ForeignKey("Rol")]
        public int RolId { get; set; }
        public Rol Rol { get; set; }
    }
}

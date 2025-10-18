using System.ComponentModel.DataAnnotations;

namespace CitasMedicasApp.Models
{
    public class Rol
    {
        [Key]
        public int RolId { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}

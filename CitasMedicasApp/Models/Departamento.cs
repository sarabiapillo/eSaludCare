using System;
using System.ComponentModel.DataAnnotations;

namespace CitasMedicasApp.Models
{
    public class Departamento
    {
        [Key]
        public int IdDepartamento { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(10)]
        public string Siglas { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}

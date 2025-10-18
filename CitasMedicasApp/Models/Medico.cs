using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitasMedicasApp.Models
{
    public class Medico
    {
        [Key]
        public int IdMedico { get; set; }

        [Required]
        public int IdUsuario { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        public Usuario Usuario { get; set; }

        public int? IdDepartamento { get; set; }
        [ForeignKey(nameof(IdDepartamento))]
        public Departamento Departamento { get; set; }

        [Required]
        [MaxLength(100)]
        public string Especialidad { get; set; }

        [Required]
        [MaxLength(50)]
        public string NumeroCedula { get; set; }

        public int ExperienciaAnios { get; set; } = 0;

        public DateTime? FechaContratacion { get; set; }
    }
}

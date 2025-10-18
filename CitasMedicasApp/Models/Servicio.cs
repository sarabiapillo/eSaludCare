using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitasMedicasApp.Models
{
    public class Servicio
    {
        [Key]
        public int IdServicio { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Costo { get; set; } = 0.00M;

        public int DuracionMinutos { get; set; } = 0;

        [ForeignKey("Departamento")]
        public int? DepartamentoAsociado { get; set; }
        public Departamento Departamento { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}

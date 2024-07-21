using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tarea
    {
        public int Id { get; set; }

        public string Identificador { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int EstadoTareaId { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [ForeignKey("EstadoTareaId")]
        public EstadoTarea EstadoTarea { get; set; }

        public ICollection<AsignacionTarea> AsignacionTarea { get; set; }
    }
}

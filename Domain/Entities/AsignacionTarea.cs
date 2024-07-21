using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AsignacionTarea
    {
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int TareaId { get; set; }

        [Required]
        public DateTime FechaAsignacion { get; set; }


        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [ForeignKey("TareaId")]
        public Tarea Tarea { get; set; }

    }
}

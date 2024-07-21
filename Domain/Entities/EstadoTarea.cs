using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EstadoTarea
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public ICollection<Tarea> Tarea { get; set; }
    }
}

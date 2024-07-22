using System.ComponentModel.DataAnnotations;

namespace Domain.Input.Task
{
    public class TareaActualizarInput
    {
        [Required]
        public string Identificador { get; set; }

        public int EstadoTareaId { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }
    }
}

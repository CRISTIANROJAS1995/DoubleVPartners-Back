
using System.ComponentModel.DataAnnotations;


namespace Domain.Input.Task
{
    public class TareaEliminarInput
    {
        [Required]
        public string Identificador { get; set; }
    }
}


namespace Domain.Dtos
{
    public class AsignacionTareaDto
    {
        public UsuarioDto Usuario { get; set; }

        public TareaDto Tarea { get; set; }

        public DateTime FechaAsignacion { get; set; }

        public UsuarioDto? UsuarioAsignador { get; set; }
    }
}

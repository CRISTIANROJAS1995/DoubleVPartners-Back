
namespace Domain.Dtos
{
    public class TareaDto
    {
        public string? Identificador { get; set; }

        public UsuarioDto Usuario { get; set; }

        public EstadoTareaDto EstadoTarea { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; } = string.Empty;
    }
}

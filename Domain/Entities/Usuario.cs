using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Identificador { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string NumeroIdentificacion { get; set; }

        public string Telefono { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }

        [ForeignKey("RoleId")]
        public Rol Rol { get; set; }

        public ICollection<Tarea> Tarea { get; set; }

        public ICollection<AsignacionTarea> AsignacionTarea { get; set; }

    }
}

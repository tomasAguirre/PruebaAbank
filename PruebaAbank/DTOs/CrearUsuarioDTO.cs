using PruebaAbank.Dominio.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PruebaAbank.DTOs
{
    public class CrearUsuarioDTO
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "El estado es obligatorio.")]
        [AllowedValues("A", "I", ErrorMessage = "El estado solo puede ser 'A' (Activo) o 'I' (Inactivo).")]
        public string Estado { get; set; }
    }
}

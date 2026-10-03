using PruebaAbank.Aplicacion.Mediador;
using PruebaAbank.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Aplicacion.CasosDeUso.Usuario.Comandos.AgregarUuario
{
    public class ComandoAgregarUsuario : IRequest
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

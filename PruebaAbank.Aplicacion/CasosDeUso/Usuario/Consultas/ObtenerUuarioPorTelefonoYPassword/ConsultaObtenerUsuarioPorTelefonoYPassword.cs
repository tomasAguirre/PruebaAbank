using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerUsuarioPorId;
using PruebaAbank.Aplicacion.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PruebaAbank.Aplicacion.Mediador.IRequest;

namespace PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerUuarioPorTelefonoYPassword
{
    public class ConsultaObtenerUsuarioPorTelefonoYPassword : IRequest<UsuarioDetalleDTO>
    {
        public string Telefono { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

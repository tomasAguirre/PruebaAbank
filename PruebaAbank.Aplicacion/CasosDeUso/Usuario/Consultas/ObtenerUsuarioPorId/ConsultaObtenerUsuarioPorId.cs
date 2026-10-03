using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerListadoUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PruebaAbank.Aplicacion.Mediador.IRequest;

namespace PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerUsuarioPorId
{
    public class ConsultaObtenerUsuarioPorId : IRequest<UsuarioDetalleDTO>
    {
        public int id { get; set; } = 0;
    }
}

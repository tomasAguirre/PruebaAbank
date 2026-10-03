using PruebaAbank.Aplicacion.Contratos.Repositorios;
using PruebaAbank.Aplicacion.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerUsuarioPorId
{
    public class CasoDeUsoObtenerUsuarioPorId : IRequestHandler<ConsultaObtenerUsuarioPorId,
                                        UsuarioDetalleDTO>
    {
        public IRepositorioUsuario RepositorioUsuario { get; }
        public CasoDeUsoObtenerUsuarioPorId(IRepositorioUsuario repositorioUsuario)
        {
            RepositorioUsuario = repositorioUsuario;
        }


        public async Task<UsuarioDetalleDTO> Handle(ConsultaObtenerUsuarioPorId request)
        {
            try
            {
                var usuario = await RepositorioUsuario.ObtenerPorId(request.id);
                if (usuario is not null) 
                {
                    var usuarioDetalleDto = usuario.ADto();
                    return usuarioDetalleDto;
                }
                else 
                {
                    return null;
                }

            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}

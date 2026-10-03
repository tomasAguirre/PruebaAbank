using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerUsuarioPorId;
using PruebaAbank.Aplicacion.Contratos.Repositorios;
using PruebaAbank.Aplicacion.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerUuarioPorTelefonoYPassword
{
    public class CasoDeUsoObtenerUsuarioPorTelefonoYPassword : IRequestHandler<ConsultaObtenerUsuarioPorTelefonoYPassword,
                                        UsuarioDetalleDTO>
    {
        public IRepositorioUsuario RepositorioUsuario { get; }

        public CasoDeUsoObtenerUsuarioPorTelefonoYPassword(IRepositorioUsuario repositorioUsuario)
        {
            RepositorioUsuario = repositorioUsuario;
        }


        public async Task<UsuarioDetalleDTO> Handle(ConsultaObtenerUsuarioPorTelefonoYPassword request)
        {
            try
            {
                var usuario = await RepositorioUsuario.ObtenerPorTelefonoYPassword(request.Telefono, request.Password);
                if (usuario is null)
                {
                    throw new Exception("Usuario no encontrado");
                }
                var usuarioDetalleDto = usuario.ADto();
                return usuarioDetalleDto;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

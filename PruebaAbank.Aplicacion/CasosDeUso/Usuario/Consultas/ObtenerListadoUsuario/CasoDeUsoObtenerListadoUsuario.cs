using PruebaAbank.Aplicacion.Contratos.Repositorios;
using PruebaAbank.Aplicacion.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerListadoUsuario
{
    public class CasoDeUsoObtenerListadoUsuario : IRequestHandler<ConsultaObtenerListadoUsuario,
                                                                            List<ListadoUsuariosDTO>>
    {
        public IRepositorioUsuario RepositorioUsuario { get; }
        public CasoDeUsoObtenerListadoUsuario(IRepositorioUsuario repositorioUsuario)
        {
            RepositorioUsuario = repositorioUsuario;
        }

        public async Task<List<ListadoUsuariosDTO>> Handle(ConsultaObtenerListadoUsuario request)
        {
            try
            {
                var usuarios = await RepositorioUsuario.obtenerTodos();
                if (usuarios is not null)
                {
                    var listadoUsuariosDto = usuarios.Select(usuario => usuario.ADto()).ToList();
                    return listadoUsuariosDto;
                }
                else
                {
                    return new List<ListadoUsuariosDTO>();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

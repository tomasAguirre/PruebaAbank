using PruebaAbank.Aplicacion.Contratos.Repositorios;
using PruebaAbank.Aplicacion.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Aplicacion.CasosDeUso.Usuario.Comandos.EliminarUsuario
{
    internal class CasoDeUsoEliminarUsuario : IRequestHandler<ComandoEliminarUsuario>
    {
        public IRepositorioUsuario RepositorioUsuario { get; }
        public CasoDeUsoEliminarUsuario(IRepositorioUsuario repositorioUsuario)
        {
            RepositorioUsuario = repositorioUsuario;
        }

        public async Task Handle(ComandoEliminarUsuario request)
        {
            try 
            {
                var usuario = RepositorioUsuario.ObtenerPorId(request.Id).Result;
                if (usuario == null)
                {
                    throw new Exception("Usuario no encontrado");
                }
                await RepositorioUsuario.borrar(usuario);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

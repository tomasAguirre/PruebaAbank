using PruebaAbank.Aplicacion.Contratos.Repositorios;
using PruebaAbank.Aplicacion.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Aplicacion.CasosDeUso.Usuario.Comandos.AgregarUuario
{
    public class CasoDeUsoAgregarUsuario : IRequestHandler<ComandoAgregarUsuario>
    {
        public IRepositorioUsuario RepositorioUsuario { get; }
        public CasoDeUsoAgregarUsuario(IRepositorioUsuario repositorioUsuario)
        {
            RepositorioUsuario = repositorioUsuario;
        }


        public async Task Handle(ComandoAgregarUsuario request)
        {
            try
            {
                Dominio.Entidades.Usuario usuario = new()
                {
                    Nombres = request.Nombres,
                    Apellidos = request.Apellidos,
                    FechaNacimiento = request.FechaNacimiento,
                    Direccion = request.Direccion,
                    Password = request.Password,
                    Telefono = request.Telefono,
                    Email = request.Email,
                    Estado = request.Estado
                };

                await RepositorioUsuario.agregar(usuario);
            }
            catch (Exception)
            {
                //await this.iunidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}

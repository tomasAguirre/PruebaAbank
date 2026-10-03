using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Comandos.AgregarUuario;
using PruebaAbank.Aplicacion.Contratos.Repositorios;
using PruebaAbank.Aplicacion.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Aplicacion.CasosDeUso.Usuario.Comandos.ActualizarUsuario
{
    public class CasoDeUsoActualizarUsuario : IRequestHandler<ComandoActualizarUsuario>
    {
        public CasoDeUsoActualizarUsuario(IRepositorioUsuario repositorioUsuario)
        {
            RepositorioUsuario = repositorioUsuario;
        }

        public IRepositorioUsuario RepositorioUsuario { get; }

        public async Task Handle(ComandoActualizarUsuario request)
        {
            try
            {
                var usuario = await RepositorioUsuario.ObtenerPorId(request.Id);
                if (usuario == null)
                {
                    throw new Exception("Usuario no encontrado");
                }
                usuario.Nombres = request.Nombres;
                usuario.Apellidos = request.Apellidos;
                usuario.FechaNacimiento = request.FechaNacimiento;
                usuario.Direccion = request.Direccion;
                usuario.Password = request.Password;
                usuario.Telefono = request.Telefono;
                usuario.Email = request.Email;

                await RepositorioUsuario.actualizar(usuario);

            }
            catch
            {
                throw;
            }
        }
    }
}

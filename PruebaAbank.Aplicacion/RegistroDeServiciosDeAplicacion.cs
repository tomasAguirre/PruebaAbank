using Microsoft.Extensions.DependencyInjection;
using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Comandos.ActualizarUsuario;
using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Comandos.AgregarUuario;
using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Comandos.EliminarUsuario;
using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerListadoUsuario;
using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerUsuarioPorId;
using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerUuarioPorTelefonoYPassword;
using PruebaAbank.Aplicacion.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Aplicacion
{
    public static class RegistroDeServiciosDeAplicacion
    {
        public static IServiceCollection AgregarServicioDeAplicacion(this IServiceCollection services)
        {
            services.AddTransient<IMediator, MediadorSimple>();
            services.AddScoped<IRequestHandler<ConsultaObtenerListadoUsuario, List<ListadoUsuariosDTO>>,
                        CasoDeUsoObtenerListadoUsuario>();
            services.AddScoped<IRequestHandler<ConsultaObtenerUsuarioPorId, UsuarioDetalleDTO>,
                        CasoDeUsoObtenerUsuarioPorId>();
            services.AddScoped<IRequestHandler<ConsultaObtenerUsuarioPorTelefonoYPassword, UsuarioDetalleDTO>,
                        CasoDeUsoObtenerUsuarioPorTelefonoYPassword>();
            services.AddScoped<IRequestHandler<ComandoEliminarUsuario>, CasoDeUsoEliminarUsuario>();
            services.AddScoped<IRequestHandler<ComandoAgregarUsuario>, CasoDeUsoAgregarUsuario>();
            services.AddScoped<IRequestHandler<ComandoActualizarUsuario>, CasoDeUsoActualizarUsuario>();

            return services;  
        }
    }
}

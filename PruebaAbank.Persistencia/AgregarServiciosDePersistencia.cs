using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using PruebaAbank.Aplicacion.Contratos.Repositorios;
using PruebaAbank.Persistencia.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Persistencia
{
    public static class AgregarServiciosDePersistencia
    {
        public static IServiceCollection AgregarServiciosDePersistenciaDapper(this IServiceCollection servicios, string connectionString)
        {
            servicios.AddScoped<IDbConnection>(sp => new NpgsqlConnection(connectionString));
            servicios.AddScoped<IRepositorioUsuario, UsuarioRepositorio>();

            return servicios;
        }

    }
}

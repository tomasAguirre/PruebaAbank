using Dapper;
using PruebaAbank.Aplicacion.Contratos.Repositorios;
using PruebaAbank.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Persistencia.Repositorios
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IRepositorioUsuario
    {
        public UsuarioRepositorio(IDbConnection dbConnection) : base(dbConnection)
        {
            this._nombreTabla = "usuarios";
        }


        public async Task actualizar(Usuario usuario)
        {
            var query = @"
        UPDATE usuarios 
        SET nombres = @Nombres,
            apellidos = @Apellidos,
            fecha_nacimiento = @FechaNacimiento,
            direccion = @Direccion,
            password = @Password,
            telefono = @Telefono,
            email = @Email
        WHERE id = @Id";

            var filasAfectadas = await _dbConnection.ExecuteAsync(query, usuario);
        }

        public async Task<PruebaAbank.Dominio.Entidades.Usuario> ObtenerPorTelefonoYPassword(string telefono, string password)
        {

            // Consulta SQL filtrando por teléfono y password, usando snake_case para la BD
            var sql = $@"
        SELECT * FROM usuarios
        WHERE telefono = @Telefono AND password = @Password
        ORDER BY id
        LIMIT 1;";

            // Pasamos los parámetros de forma segura a Dapper para evitar inyecciones SQL
            return (PruebaAbank.Dominio.Entidades.Usuario)await _dbConnection.QueryFirstOrDefaultAsync<Usuario>(sql, new
            {
                Telefono = telefono,
                Password = password
            });
        }
    }
}

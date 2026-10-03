using Dapper;
using PruebaAbank.Aplicacion.Contratos.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Persistencia.Repositorios
{
        public class Repositorio<T> : Irepositorio<T> where T : class
        {
        internal IDbConnection _dbConnection;
        internal string _nombreTabla;

        public Repositorio(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }


        public async Task<T> agregar(T entidad)
        {
            var type = entidad.GetType();

            // 1. Obtenemos las propiedades de la entidad (excluyendo Id y campos de auditoría)  
            var properties = type.GetProperties()
                .Where(p => !p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) &&
                            !p.Name.Equals("FechaCreacion", StringComparison.OrdinalIgnoreCase) &&
                            !p.Name.Equals("FechaModificacion", StringComparison.OrdinalIgnoreCase))
                .ToList();

            // 2. Usamos ToSnakeCase para transformar PascalCase en snake_case (ej: FechaNacimiento -> fecha_nacimiento)
            var columnNames = string.Join(", ", properties.Select(p => ToSnakeCase(p.Name)));

            // 3. Creamos los parámetros con '@' que Dapper leerá automáticamente de la entidad  
            var parameterNames = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            // 4. Armamos la consulta SQL dinámica apuntando a la tabla 'usuarios'
            var sql = $"INSERT INTO usuarios ({columnNames}) VALUES ({parameterNames}) RETURNING *;";

            // 5. Ejecutamos y retornamos la entidad completa devuelta por PostgreSQL
            var insertedEntity = await this._dbConnection.QuerySingleAsync<T>(sql, entidad);

            return insertedEntity;
        }

        // Método auxiliar para transformar PascalCase a snake_case de forma automática
        private static string ToSnakeCase(string input)
        {
            return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }

        public async Task borrar(T entidad)
        {
            var sql = $"DELETE FROM \"{_nombreTabla}\"  WHERE id = @Id;";
            await _dbConnection.ExecuteAsync(sql, entidad);
        }

        public async Task<int> obtenerCantidadTotalDeRegistros()
        {
            var sql = $"SELECT COUNT(*) FROM [{_nombreTabla}]";
            return await _dbConnection.ExecuteScalarAsync<int>(sql);
        }

        public async Task<T> ObtenerPorId(int id)
        {
            var sql = $"SELECT * FROM \"{_nombreTabla}\"  WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
        }

        public async Task<IEnumerable<T>> obtenerTodos()
        {

            int page = 1;
            int pageSize = 10;

            var offset = (page - 1) * pageSize;

            //SQL Server requiere ORDER BY para usar OFFSET y FETCH
            var sql = $@"
                      SELECT * FROM ""{_nombreTabla}"" 
                      ORDER BY Id
                      OFFSET @Offset ROWS
                      FETCH NEXT @PageSize ROWS ONLY";

            // Pasamos los parámetros de forma segura a Dapper
            return await _dbConnection.QueryAsync<T>(sql, new
            {
                Offset = offset,
                PageSize = pageSize
            });
        }
    }
    
}

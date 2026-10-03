using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerListadoUsuario
{
    public static class MapeadorExtensions
    {
        public static ListadoUsuariosDTO ADto(this Dominio.Entidades.Usuario usuario)
        {
            var dto = new ListadoUsuariosDTO
            {
                Id = usuario.Id,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Direccion = usuario.Direccion,
                Email = usuario.Email,
                Estado = usuario.Estado,
                FechaCreacion = usuario.FechaCreacion,
                FechaModificacion = usuario.FechaModificacion,
                FechaNacimiento = usuario.FechaNacimiento,
                Password = usuario.Password,
                Telefono = usuario.Telefono,
            };
            return dto;
        }
    }
}

using PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerListadoUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Aplicacion.CasosDeUso.Usuario.Consultas.ObtenerUsuarioPorId
{
    public static class MapeadorExtensions
    {
        public static UsuarioDetalleDTO ADto(this Dominio.Entidades.Usuario usuario)
        {
            var dto = new UsuarioDetalleDTO
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

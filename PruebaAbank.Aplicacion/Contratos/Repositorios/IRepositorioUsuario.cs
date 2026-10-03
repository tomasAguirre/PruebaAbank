using PruebaAbank.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Aplicacion.Contratos.Repositorios
{
    public interface IRepositorioUsuario : Irepositorio<Usuario>
    {
        Task<Usuario> ObtenerPorTelefonoYPassword(string telefono, string password);
        Task actualizar(Usuario usuario);
    }
}

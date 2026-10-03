using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Aplicacion.Contratos.Repositorios
{
    public interface Irepositorio<T> where T : class
    {
        Task<T> ObtenerPorId(int id);
        Task<IEnumerable<T>> obtenerTodos();
        Task<int> obtenerCantidadTotalDeRegistros();
        Task<T> agregar(T entidad);
        Task borrar(T entidad);

    }
}

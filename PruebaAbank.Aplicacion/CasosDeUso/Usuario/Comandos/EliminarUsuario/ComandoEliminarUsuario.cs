using PruebaAbank.Aplicacion.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Aplicacion.CasosDeUso.Usuario.Comandos.EliminarUsuario
{
    public class ComandoEliminarUsuario : IRequest 
    {
        public int Id { get; set; } = 0;
    }
}

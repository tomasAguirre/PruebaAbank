using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Aplicacion.Mediador
{
    public interface IRequest
    {
        public interface IRequest<TResponse>
        {
        }


        /// <summary>
        /// Class <c>IRequest</c> caso de uso que no retorna nada 
        /// </summary>
        public interface IRequest
        {
        }
    }
}

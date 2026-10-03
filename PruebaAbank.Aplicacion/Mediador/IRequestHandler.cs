using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PruebaAbank.Aplicacion.Mediador.IRequest;

namespace PruebaAbank.Aplicacion.Mediador
{
    public interface IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }

    /// <summary>
    /// Class <c>IRequestHandler</c> caso de uso que no retorna nada 
    /// </summary>
    public interface IRequestHandler<TRequest>
    where TRequest : IRequest
    {
        Task Handle(TRequest request);
    }
}

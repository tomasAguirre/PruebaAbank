using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PruebaAbank.Aplicacion.Mediador.IRequest;

namespace PruebaAbank.Aplicacion.Mediador
{
    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
        Task Send(IRequest request);
    }
}

using FluentValidation;
using PruebaAbank.Aplicacion.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PruebaAbank.Aplicacion.Mediador.IRequest;

namespace PruebaAbank.Aplicacion.Mediador
{
    public class MediadorSimple : IMediator
    {
        public readonly IServiceProvider ServiceProvider;
        public MediadorSimple(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            await this.realizarValidaciones(request);

            var tipoCasoDeUso = typeof(IRequestHandler<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            var casoDeUso = this.ServiceProvider.GetService(tipoCasoDeUso);

            if (casoDeUso is null)
            {
                throw new ExcepcionDeMediador($"No se encuentra un handler para {request.GetType().Name}");
            }

            var metodo = tipoCasoDeUso.GetMethod("Handle");

            return await (Task<TResponse>)metodo.Invoke(casoDeUso, new object[] { request })!;
        }

        //ejemplo mas sencillo ya que no retorna nada
        public async Task Send(IRequest request)
        {
            await this.realizarValidaciones(request);

            var tipoCasoDeUso = typeof(IRequestHandler<>).MakeGenericType(request.GetType());
            var casoDeUso = ServiceProvider.GetService(tipoCasoDeUso);

            if (casoDeUso is null)
            {
                throw new ExcepcionDeMediador($"No se encontro un hadler para {request.GetType().Name}");
            }

            var metodo = tipoCasoDeUso.GetMethod("Handle");
            await (Task)metodo.Invoke(casoDeUso, new object[] { request })!;
        }

        private async Task realizarValidaciones(object request)
        {
            var tipoValidador = typeof(IValidator<>).MakeGenericType(request.GetType());
            var validador = ServiceProvider.GetService(tipoValidador);

            if (validador is not null)
            {
                var metodoValidar = tipoValidador.GetMethod("ValidateAsync");
                var tareaValidar = (Task)metodoValidar!.Invoke(validador,
                                    new object[] { request, CancellationToken.None });

                await tareaValidar.ConfigureAwait(false);
                var resultado = tareaValidar.GetType().GetProperty("Result");
                var validationResult = (FluentValidation.Results.ValidationResult)resultado!.GetValue(tareaValidar)!;

                if (!validationResult.IsValid)
                {
                    throw new ExcepcionDeValidacion(validationResult);
                }
            }
        }
    }
}

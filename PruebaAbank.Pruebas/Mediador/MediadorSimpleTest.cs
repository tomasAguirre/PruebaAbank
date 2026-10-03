using NSubstitute;
using PruebaAbank.Aplicacion.Excepciones;
using PruebaAbank.Aplicacion.Mediador;
using static PruebaAbank.Aplicacion.Mediador.IRequest;

namespace PruebaAbank.Pruebas;

[TestClass]
public class MediadorSimpleTest
{
    public class RequestFalso : IRequest<string> { }
    public class HandleFalso : IRequestHandler<RequestFalso, string>
    {
        public Task<string> Handle(RequestFalso request)
        {
            return Task.FromResult("respuesta correcta");
        }
    };
    [TestMethod]
    public async Task Llama_metodoHandle()
    {
        var request = new RequestFalso();
        var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>();
        var ServiceProvider = Substitute.For<IServiceProvider>();
        ServiceProvider
            .GetService(typeof(IRequestHandler<RequestFalso, string>))
            .Returns(casoDeUsoMock);

        var mediador = new MediadorSimple(ServiceProvider);

        var resultado = await mediador.Send(request);

        await casoDeUsoMock.Received(1).Handle(request);
    }

    [TestMethod]
    [ExpectedException(typeof(ExcepcionDeMediador))]
    public async Task Send_SinHandlerRegistrado_LanzaExcepcion()
    {
        var request = new RequestFalso();
        var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>();
        var ServiceProvider = Substitute.For<IServiceProvider>();
        //ServiceProvider
        //    .GetService(typeof(IRequestHandler<RequestFalso, string>))
        //    .Returns(casoDeUsoMock);

        var mediador = new MediadorSimple(ServiceProvider);

        var resultado = await mediador.Send(request);

        //await casoDeUsoMock.Received(1).Handle(request);
    }
}

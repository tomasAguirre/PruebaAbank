using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAbank.Aplicacion.Excepciones
{
    public class ExcepcionDeValidacion : Exception
    {
        public List<string> ErroresDeValidacion { get; set; } = [];

        public ExcepcionDeValidacion(string mensajeError)
        {
            this.ErroresDeValidacion.Add(mensajeError);
        }

        public ExcepcionDeValidacion(FluentValidation.Results.ValidationResult validationResult)
        {
            foreach (var errorDeValidacion in validationResult.Errors)
            {
                this.ErroresDeValidacion.Add(errorDeValidacion.ErrorMessage);
            }
        }
    }
}

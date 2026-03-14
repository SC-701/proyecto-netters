using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class ProgramaFlujo : IProgramaFlujo
    {
        private readonly IProgramaDA _programaDA;

        public ProgramaFlujo(IProgramaDA programaDA)
        {
            _programaDA = programaDA;
        }

        public async Task<IEnumerable<ProgramaResponse>> ObtenerProgramas()
        {
            return await _programaDA.ObtenerProgramas();
        }
    }
}
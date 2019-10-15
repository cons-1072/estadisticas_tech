using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estadisticas.Model
{
    public class ResumenAmbXMedico
    {
        public string Medico { get; set; }
        public List<ResumenCodicoAmbXMedico> Codigo { get; set; }
    }

    public class ResumenCodicoAmbXMedico
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estadisticas.Model
{
    public class ResumenAmbClasscs
    {
        public string Cobertura { get; set; }
        public List<ResumenCodicoAmbClass> Codigo { get; set; }
    }

    public class ResumenCodicoAmbClass
    {
        public int Codigo { get; set; }
        public int Cantidad { get; set; }
    }
}

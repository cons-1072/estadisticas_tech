using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estadisticas.Model
{
    public class RelacionDerivacionClass
    {
        public int id_derivacion { get; set; }
        public int id_especialidad { get; set; }
        public int id_medico_derivador { get; set; }
    }

    public class RelacionDerivacionTextClass
    {
        public string id_especialidad_derivador { get; set; }
        public string id_medico_derivador { get; set; }
        public string id_especialidad_efector { get; set; }
    }

    public class RelacionProcessAmbClass
    {
        public int id_medico { get; set; }
        public int id_especialidad_derivador { get; set; }
        public int id_especialidad_efector { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estadisticas.Model
{
    public class AmbListClass
    {
        public int id_amb { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora { get; set; }
        public string cobertura { get; set; }
        public string paciente { get; set; }
        public string especialidad { get; set; }
        public int codigo { get; set; }
        public string descripcion { get; set; }
        public int cantidad { get; set; }
        public string efector { get; set; }
        public string derivador { get; set; }
        public string derivador_matricula { get; set; }
        public string grupo { get; set; }        
    }
}

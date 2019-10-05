using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estadisticas.Model
{
    public class AmbClass
    {
        public int id_amb { get; set; }
        public int grupo_de_turno { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora { get; set; }
        public Boolean vino { get; set; }
        public string operador { get; set; }
        public string ope_recepcion { get; set; }
        public DateTime sistema { get; set; }
        public Boolean anulado { get; set; }
        public string carnet { get; set; }
        public string vo { get; set; }
        public string cobertura { get; set; }
        public int codigo_cobertura { get; set; }
        public string plan { get; set; }
        public string sucursal { get; set; }
        public string consultorio { get; set; }
        public string matricula { get; set; }
        public string medico { get; set; }
        public string paciente { get; set; }
        public string sexo { get; set; }
        public string nro_doc { get; set; }
        public string tip_doc { get; set; }
        public DateTime fec_nac { get; set; }
        public string ecivil { get; set; }
        public string idprovincia { get; set; }
        public string calle { get; set; }
        public string numero { get; set; }
        public string cp { get; set; }
        public int hc { get; set; }
        public string telefono { get; set; }
        public string depto { get; set; }
        public string piso { get; set; }
        public string email { get; set; }
        public DateTime alta_sistema { get; set; }
        public string especialidad { get; set; }
        public string serv_espe { get; set; }
        public int codigo { get; set; }
        public string descripcion { get; set; }
        public int cantidad { get; set; }
        public float coseguro { get; set; }
        public float reintegro { get; set; }
        public float imp_no_cubierto { get; set; }
        public string efector { get; set; }
        public string efector_matricula { get; set; }
        public string efector_codigo { get; set; }
        public string derivador { get; set; }
        public string derivador_matricula { get; set; }
        public string diagnostico { get; set; }
        public string grupo { get; set; }
        public string sector { get; set; }
        public string centrocosto { get; set; }
        public DateTime periodo { get; set; }
        public string cob_factura_letra { get; set; }
        public string cob_factura_ptoventa { get; set; }
        public int cob_factura_numero { get; set; }
        public string caja_letra { get; set; }
        public int caja_num4 { get; set; }
        public int caja_num8 { get; set; }
    }
}

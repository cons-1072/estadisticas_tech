using Npgsql;
using System;
using System.Data;

namespace Estadisticas.Dao
{
    public class DaoAmbEstadistic
    {
        conexion Conexion = new conexion();
        public Boolean Insert_Amb_Sql_Class(DataRow ambClass)
        {
            Boolean retorno = false;
            NpgsqlConnection conn = Conexion.Crear_conexion();
            try
            {
                string sql = "INSERT INTO amb_estadistic_tech (grupo_de_turno,fecha,hora,vino,operador,ope_recepcion"
                    +",sistema,anulado,carnet,vo,cobertura,codigo_cobertura,plan,sucursal,consultorio,matricula,medico"
                    +",paciente,sexo,nro_doc,tip_doc,fec_nac,ecivil,idprovincia,calle,numero,cp,hc,telefono,depto,piso"
                    +",email,alta_sistema,especialidad,serv_espe,codigo,descripcion,cantidad,coseguro,reintegro,imp_no_cubierto"
                    +",efector,efector_matricula,efector_codigo,derivador,derivador_matricula,diagnostico,grupo,sector,centrocosto"
                    +",periodo,cob_factura_letra,cob_factura_ptoventa,cob_factura_numero,caja_letra,caja_num4,caja_num8) "
                    + "VALUES (@grupo_de_turno,@fecha,@hora,@vino,@operador,@ope_recepcion"
                    + ",@sistema,@anulado,@carnet,@vo,@cobertura,@codigo_cobertura,@plan,@sucursal,@consultorio,@matricula,@medico"
                    + ",@paciente,@sexo,@nro_doc,@tip_doc,@fec_nac,@ecivil,@idprovincia,@calle,@numero,@cp,@hc,@telefono,@depto,@piso"
                    + ",@email,@alta_sistema,@especialidad,@serv_espe,@codigo,@descripcion,@cantidad,@coseguro,@reintegro,@imp_no_cubierto"
                    + ",@efector,@efector_matricula,@efector_codigo,@derivador,@derivador_matricula,@diagnostico,@grupo,@sector,@centrocosto"
                    + ",@periodo,@cob_factura_letra,@cob_factura_ptoventa,@cob_factura_numero,@caja_letra,@caja_num4,@caja_num8)";

                NpgsqlCommand cmd = new NpgsqlCommand()
                {
                    Connection = conn,
                    CommandText = sql,
                    CommandType = CommandType.Text
                };
                //cmd.Parameters.Add(new NpgsqlParameter("@Id_Padron", padron.Id_Padron));
                cmd.Parameters.Add(new NpgsqlParameter("@grupo_de_turno", ambClass[1])); //.grupo_de_turno));
                cmd.Parameters.Add(new NpgsqlParameter("@fecha", ambClass[2]));//.fecha));
                cmd.Parameters.Add(new NpgsqlParameter("@hora", ambClass[3])); //.hora));
                cmd.Parameters.Add(new NpgsqlParameter("@vino", ambClass[4])); //.vino));
                cmd.Parameters.Add(new NpgsqlParameter("@operador", ambClass[5])); //.operador));
                cmd.Parameters.Add(new NpgsqlParameter("@ope_recepcion", ambClass[6])); //.ope_recepcion));
                cmd.Parameters.Add(new NpgsqlParameter("@sistema", ambClass[7])); //.sistema));
                cmd.Parameters.Add(new NpgsqlParameter("@anulado", ambClass[8])); //.anulado));
                cmd.Parameters.Add(new NpgsqlParameter("@carnet", ambClass[9])); //.carnet));
                cmd.Parameters.Add(new NpgsqlParameter("@vo", ambClass[10])); //.vo));
                cmd.Parameters.Add(new NpgsqlParameter("@cobertura", ambClass[11])); //.cobertura));
                cmd.Parameters.Add(new NpgsqlParameter("@codigo_cobertura", ambClass[12])); //.codigo_cobertura));
                cmd.Parameters.Add(new NpgsqlParameter("@plan", ambClass[13])); //.plan));
                cmd.Parameters.Add(new NpgsqlParameter("@sucursal", ambClass[14])); //.sucursal));
                cmd.Parameters.Add(new NpgsqlParameter("@consultorio", ambClass[15])); //.consultorio));
                cmd.Parameters.Add(new NpgsqlParameter("@matricula", ambClass[16])); //.matricula));
                cmd.Parameters.Add(new NpgsqlParameter("@medico", ambClass[17])); //.medico));
                cmd.Parameters.Add(new NpgsqlParameter("@paciente", ambClass[18])); //.paciente));
                cmd.Parameters.Add(new NpgsqlParameter("@sexo", ambClass[19])); //.sexo));
                cmd.Parameters.Add(new NpgsqlParameter("@nro_doc", ambClass[20])); //.nro_doc));
                cmd.Parameters.Add(new NpgsqlParameter("@tip_doc", ambClass[21])); //.tip_doc));
                cmd.Parameters.Add(new NpgsqlParameter("@fec_nac", ambClass[22])); //.fec_nac));
                cmd.Parameters.Add(new NpgsqlParameter("@ecivil", ambClass[23])); //.ecivil));
                cmd.Parameters.Add(new NpgsqlParameter("@idprovincia", ambClass[24])); //.idprovincia));
                cmd.Parameters.Add(new NpgsqlParameter("@calle", ambClass[25])); //.calle));
                cmd.Parameters.Add(new NpgsqlParameter("@numero", ambClass[26])); //.numero));
                cmd.Parameters.Add(new NpgsqlParameter("@cp", ambClass[27])); //.cp));
                cmd.Parameters.Add(new NpgsqlParameter("@hc", ambClass[28])); //.hc));
                cmd.Parameters.Add(new NpgsqlParameter("@telefono", ambClass[29])); //.telefono));
                cmd.Parameters.Add(new NpgsqlParameter("@depto", ambClass[30])); //.depto));
                cmd.Parameters.Add(new NpgsqlParameter("@piso", ambClass[31])); //.piso));
                cmd.Parameters.Add(new NpgsqlParameter("@email", ambClass[32])); //.email));
                cmd.Parameters.Add(new NpgsqlParameter("@alta_sistema", ambClass[33])); //.alta_sistema));
                cmd.Parameters.Add(new NpgsqlParameter("@especialidad", ambClass[34])); //.especialidad));
                cmd.Parameters.Add(new NpgsqlParameter("@serv_espe", ambClass[35])); //.serv_espe));
                cmd.Parameters.Add(new NpgsqlParameter("@codigo", ambClass[36])); //.codigo));
                cmd.Parameters.Add(new NpgsqlParameter("@descripcion", ambClass[37])); //.descripcion));
                cmd.Parameters.Add(new NpgsqlParameter("@cantidad", ambClass[38])); //.cantidad));
                cmd.Parameters.Add(new NpgsqlParameter("@coseguro", ambClass[39])); //.coseguro));
                cmd.Parameters.Add(new NpgsqlParameter("@reintegro", ambClass[40])); //.reintegro));
                cmd.Parameters.Add(new NpgsqlParameter("@imp_no_cubierto", ambClass[41])); //.imp_no_cubierto));
                cmd.Parameters.Add(new NpgsqlParameter("@efector", ambClass[42])); //.efector));
                cmd.Parameters.Add(new NpgsqlParameter("@efector_matricula", ambClass[43])); //.efector_matricula));
                cmd.Parameters.Add(new NpgsqlParameter("@efector_codigo", ambClass[44])); //.efector_codigo));
                cmd.Parameters.Add(new NpgsqlParameter("@derivador", ambClass[45])); //.derivador));
                cmd.Parameters.Add(new NpgsqlParameter("@derivador_matricula", ambClass[46])); //.derivador_matricula));
                cmd.Parameters.Add(new NpgsqlParameter("@diagnostico", ambClass[47])); //.diagnostico));
                cmd.Parameters.Add(new NpgsqlParameter("@grupo", ambClass[48])); //.grupo));
                cmd.Parameters.Add(new NpgsqlParameter("@sector", ambClass[49])); //.sector));
                cmd.Parameters.Add(new NpgsqlParameter("@centrocosto", ambClass[50])); //.centrocosto));
                cmd.Parameters.Add(new NpgsqlParameter("@periodo", ambClass[51])); //.periodo));
                cmd.Parameters.Add(new NpgsqlParameter("@cob_factura_letra", ambClass[52])); //.cob_factura_letra));
                cmd.Parameters.Add(new NpgsqlParameter("@cob_factura_ptoventa", ambClass[53])); //.cob_factura_ptoventa));
                cmd.Parameters.Add(new NpgsqlParameter("@cob_factura_numero", ambClass[54])); //.cob_factura_numero));
                cmd.Parameters.Add(new NpgsqlParameter("@caja_letra", ambClass[55])); //.caja_letra));
                cmd.Parameters.Add(new NpgsqlParameter("@caja_num4", ambClass[56])); //.caja_num4));
                cmd.Parameters.Add(new NpgsqlParameter("@caja_num8", ambClass[57])); //.caja_num8));
                //cmd.Parameters.Add(new NpgsqlParameter("@Enfermedades", padron.enfermedades));*/
                cmd.ExecuteNonQuery();
                retorno = true;
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                // something went wrong, and you wanna know why
                retorno = false;
                throw;
            }
            return retorno;
        }
    }
}

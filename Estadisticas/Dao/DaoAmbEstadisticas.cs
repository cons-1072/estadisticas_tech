using Estadisticas.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Estadisticas.Dao
{
    public class DaoAmbEstadisticas
    {
        conexion Conexion = new conexion();

        public List<AmbListClass> Estadisticas_Ambulatoria(IList<MedicosDerivadoresClass> list_selection_med, IList<EspecialidadesDerivadoresClass> list_selection_derivador, IList<EspecialidadesEfectoresClass> list_selection_efector)
        {
            DaoMedicosDerivadores daoMedicosDerivadores = new DaoMedicosDerivadores();
            DaoEspecialidadesDerivadores daoEspecialidadesDerivadores = new DaoEspecialidadesDerivadores();
            DaoEspecialidadesEfector daoEspecialidadesEfector = new DaoEspecialidadesEfector();
            List<AmbListClass> List_Retorno = new List<AmbListClass>();
            List<AmbListClass> List_Retorno_Final = new List<AmbListClass>();
            NpgsqlConnection conn = Conexion.Crear_conexion();
            try
            {
                if (list_selection_med != null && list_selection_derivador == null)
                {
                    foreach (MedicosDerivadoresClass medico in list_selection_med)
                    {
                        if (medico.Medico != "")
                        {
                            List<AmbListClass> List_Retorno_tmp = new List<AmbListClass>();
                            string sql = "SELECT fecha,paciente,cobertura,codigo,descripcion,cantidad,efector,especialidad,derivador,grupo,vino FROM amb_estadistic_tech "
                                        + "WHERE derivador = @Derivador AND vino = true Order By fecha, derivador";

                            NpgsqlCommand cmd = new NpgsqlCommand()
                            {
                                Connection = conn,
                                CommandText = sql,
                                CommandType = CommandType.Text
                            };
                            //cmd.Parameters.Add(new NpgsqlParameter("@Especialidad", .Trim().ToUpper()));
                            cmd.Parameters.Add(new NpgsqlParameter("@Derivador", medico.Medico.Trim().ToUpper()));
                            NpgsqlDataReader reader = cmd.ExecuteReader();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    AmbListClass list = new AmbListClass();
                                    if (!reader.IsDBNull(0)) { list.fecha = reader.GetDateTime(0); };
                                    if (!reader.IsDBNull(1)) { list.paciente = reader.GetString(1).Trim(); };
                                    if (!reader.IsDBNull(2)) { list.cobertura = reader.GetString(2).Trim(); };
                                    if (!reader.IsDBNull(3)) { list.codigo = reader.GetInt32(3); };
                                    if (!reader.IsDBNull(4)) { list.descripcion = reader.GetString(4).Trim(); };
                                    if (!reader.IsDBNull(5)) { list.cantidad = reader.GetInt32(5); };
                                    if (!reader.IsDBNull(6)) { list.efector = reader.GetString(6).Trim(); };
                                    if (!reader.IsDBNull(7)) { list.especialidad = reader.GetString(7).Trim(); };
                                    if (!reader.IsDBNull(8)) { list.derivador = reader.GetString(8).Trim(); };
                                    if (!reader.IsDBNull(9)) { list.grupo = reader.GetString(9).Trim(); };
                                    List_Retorno_tmp.Add(list);
                                }
                            }
                            reader.Close();
                            //conn.Close();
                            List_Retorno.AddRange(List_Retorno_tmp);
                        }
                    }
                }
                if (list_selection_med == null && list_selection_derivador != null)
                {
                    DaoRelacionDerivadores daoRelacionDerivadores = new DaoRelacionDerivadores();
                    List<RelacionDerivacionClass> relacion = daoRelacionDerivadores.list_relacion(list_selection_derivador);
                    foreach (RelacionDerivacionClass medico in relacion)
                    {
                        if (medico.id_medico_derivador != 0)
                        {
                            List<AmbListClass> List_Retorno_tmp = new List<AmbListClass>();
                            string sql = "SELECT fecha,paciente,cobertura,codigo,descripcion,cantidad,efector,especialidad,derivador,grupo,vino FROM amb_estadistic_tech "
                                        + "WHERE derivador = @Derivador AND vino = true Order By fecha, derivador";

                            NpgsqlCommand cmd = new NpgsqlCommand()
                            {
                                Connection = conn,
                                CommandText = sql,
                                CommandType = CommandType.Text
                            };
                            //cmd.Parameters.Add(new NpgsqlParameter("@Especialidad", .Trim().ToUpper()));
                            cmd.Parameters.Add(new NpgsqlParameter("@Derivador", daoMedicosDerivadores.Find_Medico_By_Id(medico.id_medico_derivador)));
                            NpgsqlDataReader reader = cmd.ExecuteReader();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    AmbListClass list = new AmbListClass();
                                    if (!reader.IsDBNull(0)) { list.fecha = reader.GetDateTime(0); };
                                    if (!reader.IsDBNull(1)) { list.paciente = reader.GetString(1).Trim(); };
                                    if (!reader.IsDBNull(2)) { list.cobertura = reader.GetString(2).Trim(); };
                                    if (!reader.IsDBNull(3)) { list.codigo = reader.GetInt32(3); };
                                    if (!reader.IsDBNull(4)) { list.descripcion = reader.GetString(4).Trim(); };
                                    if (!reader.IsDBNull(5)) { list.cantidad = reader.GetInt32(5); };
                                    if (!reader.IsDBNull(6)) { list.efector = reader.GetString(6).Trim(); };
                                    if (!reader.IsDBNull(7)) { list.especialidad = reader.GetString(7).Trim(); };
                                    if (!reader.IsDBNull(8)) { list.derivador = reader.GetString(8).Trim(); };
                                    if (!reader.IsDBNull(9)) { list.grupo = reader.GetString(9).Trim(); };
                                    List_Retorno_tmp.Add(list);
                                }
                            }
                            reader.Close();
                            //conn.Close();
                            List_Retorno.AddRange(List_Retorno_tmp);
                        }
                    }
                }
                conn.Close();

                //Filtro por especialidad efectora
                if (list_selection_efector != null)
                {
                    List_Retorno = List_Retorno.Except(List_Retorno.Where(a => !list_selection_efector.Any(y => y.Especialidad_Efector == a.especialidad))).ToList();
                    //List_Retorno = List_Retorno.Where(x => !list_selection_efector.Any(y => y.Especialidad_Efector == x.especialidad)).ToList();                    
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                // something went wrong, and you wanna know why
                throw;
            }
            return List_Retorno;
        }
    }
}

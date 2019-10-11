using Estadisticas.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Estadisticas.Dao
{
    public class DaoRelacionDerivadores
    {
        conexion Conexion = new conexion();

        public Boolean Insert_RelacionDerivador(RelacionDerivacionClass relacion)
        {
            Boolean retorno = false;
            NpgsqlConnection conn = Conexion.Crear_conexion();
            try
            {
                string sql = "INSERT INTO relacion_derivadores (id_especialidad, id_medico_derivador) VALUES (@Id_especialidad, @Id_medico_derivador)";

                NpgsqlCommand cmd = new NpgsqlCommand()
                {
                    Connection = conn,
                    CommandText = sql,
                    CommandType = CommandType.Text
                };
                //cmd.Parameters.Add(new NpgsqlParameter("@Id_Padron", padron.Id_Padron));
                cmd.Parameters.Add(new NpgsqlParameter("@Id_especialidad", relacion.id_especialidad)); //.grupo_de_turno));
                cmd.Parameters.Add(new NpgsqlParameter("@Id_medico_derivador", relacion.id_medico_derivador));//.fecha));
                cmd.ExecuteNonQuery();
                retorno = true;
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                conn.Close();
                // something went wrong, and you wanna know why
                retorno = false;
                throw;
            }
            return retorno;
        }

        public Boolean Check_RelacionDerivador(int id_especialidad, int id_medico_derivador)
        {
            Boolean retorno = false;
            NpgsqlConnection conn = Conexion.Crear_conexion();
            try
            {
                string sql = "SELECT * FROM relacion_derivadores WHERE id_especialidad = @Id_especialidad AND id_medico_derivador = @Id_medico_derivador";

                NpgsqlCommand cmd = new NpgsqlCommand()
                {
                    Connection = conn,
                    CommandText = sql,
                    CommandType = CommandType.Text
                };
                //cmd.Parameters.Add(new NpgsqlParameter("@Id_Padron", padron.Id_Padron));
                cmd.Parameters.Add(new NpgsqlParameter("@Id_especialidad", id_especialidad));
                cmd.Parameters.Add(new NpgsqlParameter("@Id_medico_derivador", id_medico_derivador));
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetInt32(1) == id_especialidad && reader.GetInt32(2) == id_medico_derivador)
                        {
                            retorno = true;
                        }
                    }
                }
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                conn.Close();
                // something went wrong, and you wanna know why
                retorno = false;
                throw;
            }
            return retorno;
        }

        public List<RelacionDerivacionClass> list_relacion(IList<EspecialidadesDerivadoresClass> list_selection_derivador)
        {
            List<RelacionDerivacionClass> List_Retorno = new List<RelacionDerivacionClass>();
            NpgsqlConnection conn = Conexion.Crear_conexion();
            try
            {
                foreach (EspecialidadesDerivadoresClass relacion_select in list_selection_derivador)
                {
                    string sql = "SELECT * FROM relacion_derivadores WHERE id_especialidad = @Id_especialidad";

                    NpgsqlCommand cmd = new NpgsqlCommand()
                    {
                        Connection = conn,
                        CommandText = sql,
                        CommandType = CommandType.Text
                    };
                    //cmd.Parameters.Add(new NpgsqlParameter("@Id_Padron", padron.Id_Padron));
                    cmd.Parameters.Add(new NpgsqlParameter("@Id_especialidad", relacion_select.id_especialidad));
                    //cmd.Parameters.Add(new NpgsqlParameter("@Id_medico_derivador", id_medico_derivador));
                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RelacionDerivacionClass relacion = new RelacionDerivacionClass();
                            if (!reader.IsDBNull(0)) { relacion.id_derivacion = reader.GetInt32(0); }
                            if (!reader.IsDBNull(1)) { relacion.id_especialidad = reader.GetInt32(1); }
                            if (!reader.IsDBNull(2)) { relacion.id_medico_derivador = reader.GetInt32(2); }
                            List_Retorno.Add(relacion);
                        }
                    }
                    cmd.Dispose();
                }                
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                conn.Close();
                // something went wrong, and you wanna know why
                throw;
            }
            return List_Retorno;
        }
    }
}

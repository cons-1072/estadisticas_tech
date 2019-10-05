using Estadisticas.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Estadisticas.Dao
{
    public class DaoEspecialidadesEfector
    {
        conexion Conexion = new conexion();
        public Boolean Insert_EspecialidadEfector(EspecialidadesEfectoresClass especialidades)
        {
            Boolean retorno = false;
            NpgsqlConnection conn = Conexion.Crear_conexion();
            try
            {
                string sql = "INSERT INTO especialidades_efectores (especialidad_efector) VALUES (@Especialidades)";

                NpgsqlCommand cmd = new NpgsqlCommand()
                {
                    Connection = conn,
                    CommandText = sql,
                    CommandType = CommandType.Text
                };
                //cmd.Parameters.Add(new NpgsqlParameter("@Id_Padron", padron.Id_Padron));
                cmd.Parameters.Add(new NpgsqlParameter("@Especialidades", especialidades.Especialidad_Efector)); //.grupo_de_turno));
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

        public Boolean Check_EspecialidadEfector(string Especialidad)
        {
            Boolean retorno = false;
            NpgsqlConnection conn = Conexion.Crear_conexion();
            try
            {
                string sql = "SELECT * FROM especialidades_efectores WHERE especialidad_efector = @Especialidad";

                NpgsqlCommand cmd = new NpgsqlCommand()
                {
                    Connection = conn,
                    CommandText = sql,
                    CommandType = CommandType.Text
                };
                //cmd.Parameters.Add(new NpgsqlParameter("@Id_Padron", padron.Id_Padron));
                cmd.Parameters.Add(new NpgsqlParameter("@Especialidad", Especialidad)); //.grupo_de_turno));
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(1).Trim() == Especialidad.Trim())
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
                conn.Close();
                // something went wrong, and you wanna know why
                retorno = false;
                throw;
            }
            return retorno;
        }

        public List<EspecialidadesEfectoresClass> Find_All()
        {
            // define connection string and query
            //"Data Source=ADMINISTRADOR-P\\SQLEXPRESS;Initial Catalog=Empleados;Integrated Security=True";
            string query = "SELECT * FROM especialidades_efectores";
            //int rowsAffected = 0;
            List<EspecialidadesEfectoresClass> List_Retorno = new List<EspecialidadesEfectoresClass>();
            // create connection and command in "using" blocks
            using (NpgsqlConnection conn = Conexion.Crear_conexion())
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
            {
                //cmd.Parameters.AddWithValue("@Legajo", legajo);
                //cmd.Parameters.AddWithValue("@Desde", desde);
                //cmd.Parameters.AddWithValue("@Hasta", hasta);
                //conn.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EspecialidadesEfectoresClass especialidad = new EspecialidadesEfectoresClass();
                        if (!reader.IsDBNull(0)) { especialidad.id_especialidad = Convert.ToInt32(reader.GetValue(0)); };
                        if (!reader.IsDBNull(1)) { especialidad.Especialidad_Efector = reader.GetString(1); };
                        List_Retorno.Add(especialidad);
                    }
                }
                reader.Close();
                conn.Close();
            }
            List_Retorno.Sort((p, q) => string.Compare(p.Especialidad_Efector, q.Especialidad_Efector));
            return List_Retorno;
        }

        public Task<EspecialidadesEfectoresClass[]> GetEspecialidadEfectorAsync()
        {
            // define connection string and query
            //"Data Source=ADMINISTRADOR-P\\SQLEXPRESS;Initial Catalog=Empleados;Integrated Security=True";
            string query = "SELECT * FROM especialidades_efectores";
            //int rowsAffected = 0;
            List<EspecialidadesEfectoresClass> List_Retorno = new List<EspecialidadesEfectoresClass>();
            // create connection and command in "using" blocks
            using (NpgsqlConnection conn = Conexion.Crear_conexion())
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
            {
                //cmd.Parameters.AddWithValue("@Legajo", legajo);
                //cmd.Parameters.AddWithValue("@Desde", desde);
                //cmd.Parameters.AddWithValue("@Hasta", hasta);
                //conn.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EspecialidadesEfectoresClass especialidad = new EspecialidadesEfectoresClass();
                        if (!reader.IsDBNull(0)) { especialidad.id_especialidad = Convert.ToInt32(reader.GetValue(0)); };
                        if (!reader.IsDBNull(1)) { especialidad.Especialidad_Efector = reader.GetString(1); };
                        List_Retorno.Add(especialidad);
                    }
                }
                reader.Close();
                conn.Close();
            }
            List_Retorno.Sort((p, q) => string.Compare(p.Especialidad_Efector, q.Especialidad_Efector));
            return Task.FromResult(List_Retorno.ToArray());
        }

        public string GetEspecialidadEfectorTxt(int id_especialidad)
        {
            string tmp_retorno = "";
            // define connection string and query
            //"Data Source=ADMINISTRADOR-P\\SQLEXPRESS;Initial Catalog=Empleados;Integrated Security=True";
            string query = "SELECT * FROM especialidades_efectores WHERE id_especialidad = @Id_Especialidad";
            //int rowsAffected = 0;
            List<EspecialidadesEfectoresClass> List_Retorno = new List<EspecialidadesEfectoresClass>();
            // create connection and command in "using" blocks
            using (NpgsqlConnection conn = Conexion.Crear_conexion())
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.Add(new NpgsqlParameter("@Id_Especialidad", id_especialidad));
                //cmd.Parameters.AddWithValue("@Legajo", legajo);
                //cmd.Parameters.AddWithValue("@Desde", desde);
                //cmd.Parameters.AddWithValue("@Hasta", hasta);
                //conn.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(1)) { tmp_retorno = reader.GetString(1).Trim(); };
                    }
                }
                reader.Close();
                conn.Close();
            }
            return tmp_retorno;
        }
    }
}

using Estadisticas.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Estadisticas.Dao
{
    public class DaoEspecialidadesDerivadores
    {
        conexion Conexion = new conexion();
        public Boolean Insert_EspecialidadDerivador(EspecialidadesDerivadoresClass especialidades)
        {
            Boolean retorno = false;
            NpgsqlConnection conn = Conexion.Crear_conexion();
            try
            {
                string sql = "INSERT INTO especialidades_derivadores (Especialidad) VALUES (@Especialidades)";

                NpgsqlCommand cmd = new NpgsqlCommand()
                {
                    Connection = conn,
                    CommandText = sql,
                    CommandType = CommandType.Text
                };
                //cmd.Parameters.Add(new NpgsqlParameter("@Id_Padron", padron.Id_Padron));
                cmd.Parameters.Add(new NpgsqlParameter("@Especialidades", especialidades.Especialidad)); //.grupo_de_turno));
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

        public Boolean Check_EspecialidadDerivador(string Especialidad)
        {
            Boolean retorno = false;
            NpgsqlConnection conn = Conexion.Crear_conexion();
            try
            {
                string sql = "SELECT * FROM especialidades_derivadores WHERE Especialidad = @Especialidad";

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
                Console.WriteLine(ex.Message);
                conn.Close();
                // something went wrong, and you wanna know why
                retorno = false;
                throw;
            }
            return retorno;
        }

        public List<EspecialidadesDerivadoresClass> Find_All()
        {
            // define connection string and query
            //"Data Source=ADMINISTRADOR-P\\SQLEXPRESS;Initial Catalog=Empleados;Integrated Security=True";
            string query = "SELECT * FROM especialidades_derivadores";
            //int rowsAffected = 0;
            List<EspecialidadesDerivadoresClass> List_Retorno = new List<EspecialidadesDerivadoresClass>();
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
                        EspecialidadesDerivadoresClass especialidad = new EspecialidadesDerivadoresClass();
                        if (!reader.IsDBNull(0)) { especialidad.id_especialidad = Convert.ToInt32(reader.GetValue(0)); };
                        if (!reader.IsDBNull(1)) { especialidad.Especialidad = reader.GetString(1); };
                        List_Retorno.Add(especialidad);
                    }
                }
                reader.Close();
                conn.Close();
            }
            List_Retorno.Sort((p, q) => string.Compare(p.Especialidad, q.Especialidad));
            return List_Retorno;
        }

        public Task<EspecialidadesDerivadoresClass[]> GetEspecialidadAsync()
        {
            // define connection string and query
            //"Data Source=ADMINISTRADOR-P\\SQLEXPRESS;Initial Catalog=Empleados;Integrated Security=True";
            string query = "SELECT * FROM especialidades_derivadores";
            //int rowsAffected = 0;
            List<EspecialidadesDerivadoresClass> List_Retorno = new List<EspecialidadesDerivadoresClass>();
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
                        EspecialidadesDerivadoresClass especialidad = new EspecialidadesDerivadoresClass();
                        if (!reader.IsDBNull(0)) { especialidad.id_especialidad = Convert.ToInt32(reader.GetValue(0)); };
                        if (!reader.IsDBNull(1)) { especialidad.Especialidad = reader.GetString(1); };
                        List_Retorno.Add(especialidad);
                    }
                }
                reader.Close();
                conn.Close();
            }
            List_Retorno.Sort((p, q) => string.Compare(p.Especialidad, q.Especialidad));
            return Task.FromResult(List_Retorno.ToArray());
        }

        public string GetEspecialidadTxt(int id_especialidad)
        {
            string tmp_retorno = "";
            // define connection string and query
            //"Data Source=ADMINISTRADOR-P\\SQLEXPRESS;Initial Catalog=Empleados;Integrated Security=True";
            string query = "SELECT * FROM especialidades_derivadores WHERE id_especialidad = @Id_Especialidad";
            //int rowsAffected = 0;
            List<EspecialidadesDerivadoresClass> List_Retorno = new List<EspecialidadesDerivadoresClass>();
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
                        if (!reader.IsDBNull(1)) {tmp_retorno = reader.GetString(1).Trim(); };
                    }
                }
                reader.Close();
                conn.Close();
            }
            return tmp_retorno;
        }
    }
}

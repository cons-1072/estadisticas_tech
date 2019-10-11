using Estadisticas.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Estadisticas.Dao
{
    public class DaoMedicosDerivadores
    {
        conexion Conexion = new conexion();
        public Boolean Insert_MedicoDerivador(MedicosDerivadoresClass Medico)
        {
            Boolean retorno = false;
            NpgsqlConnection conn = Conexion.Crear_conexion();
            try
            {
                string sql = "INSERT INTO medicos_derivadores (medico, matricula) VALUES (@Medico, @Matricula)";

                NpgsqlCommand cmd = new NpgsqlCommand()
                {
                    Connection = conn,
                    CommandText = sql,
                    CommandType = CommandType.Text
                };
                //cmd.Parameters.Add(new NpgsqlParameter("@Id_Padron", padron.Id_Padron));
                cmd.Parameters.Add(new NpgsqlParameter("@Medico", Medico.Medico)); //.grupo_de_turno));
                cmd.Parameters.Add(new NpgsqlParameter("@Matricula", Medico.Matricula));//.fecha));
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

        public Boolean Check_MedicoDerivador(string Medico)
        {
            Boolean retorno = false;
            NpgsqlConnection conn = Conexion.Crear_conexion();
            try
            {
                string sql = "SELECT * FROM medicos_derivadores WHERE Medico = @Medico";

                NpgsqlCommand cmd = new NpgsqlCommand()
                {
                    Connection = conn,
                    CommandText = sql,
                    CommandType = CommandType.Text
                };
                //cmd.Parameters.Add(new NpgsqlParameter("@Id_Padron", padron.Id_Padron));
                cmd.Parameters.Add(new NpgsqlParameter("@Medico", Medico)); //.grupo_de_turno));
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if(reader.GetString(1).Trim() == Medico.Trim())
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

        public List<MedicosDerivadoresClass> Find_All()
        {
            // define connection string and query
            //"Data Source=ADMINISTRADOR-P\\SQLEXPRESS;Initial Catalog=Empleados;Integrated Security=True";
            string query = "SELECT * FROM Medicos_derivadores";
            //int rowsAffected = 0;
            List<MedicosDerivadoresClass> List_Retorno = new List<MedicosDerivadoresClass>();
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
                        MedicosDerivadoresClass medico = new MedicosDerivadoresClass();
                        if (!reader.IsDBNull(0)) { medico.id_medico_derivador = Convert.ToInt32(reader.GetValue(0)); };
                        if (!reader.IsDBNull(1)) { medico.Medico = reader.GetString(1); };
                        if (!reader.IsDBNull(2)) { medico.Matricula = reader.GetString(2); };
                        List_Retorno.Add(medico);
                    }
                }
                reader.Close();
                conn.Close();
            }
            List_Retorno.Sort((p, q) => string.Compare(p.Medico, q.Medico));
            return List_Retorno;
        }

        public string Find_Medico_By_Id(int id_medico)
        {
            string retorno ="";
            // define connection string and query
            //"Data Source=ADMINISTRADOR-P\\SQLEXPRESS;Initial Catalog=Empleados;Integrated Security=True";
            string query = "SELECT * FROM Medicos_derivadores WHERE id_medico_derivador = @Id_Medico";
            //int rowsAffected = 0;
            // create connection and command in "using" blocks
            using (NpgsqlConnection conn = Conexion.Crear_conexion())
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
            {
                //cmd.Parameters.AddWithValue("@Legajo", legajo);
                //cmd.Parameters.AddWithValue("@Desde", desde);
                cmd.Parameters.AddWithValue("@Id_Medico", id_medico);
                //conn.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(1)) { retorno = reader.GetString(1).Trim(); };                        
                    }
                }
                reader.Close();
                conn.Close();
            }
            return retorno;
        }

        public Task<MedicosDerivadoresClass[]> GetMedicosAsync()
        {
            // define connection string and query
            //"Data Source=ADMINISTRADOR-P\\SQLEXPRESS;Initial Catalog=Empleados;Integrated Security=True";
            string query = "SELECT * FROM Medicos_derivadores";
            //int rowsAffected = 0;
            List<MedicosDerivadoresClass> List_Retorno = new List<MedicosDerivadoresClass>();
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
                        MedicosDerivadoresClass medico = new MedicosDerivadoresClass();
                        if (!reader.IsDBNull(0)) { medico.id_medico_derivador = Convert.ToInt32(reader.GetValue(0)); };
                        if (!reader.IsDBNull(1)) { medico.Medico = reader.GetString(1); };
                        if (!reader.IsDBNull(2)) { medico.Matricula = reader.GetString(2); };
                        List_Retorno.Add(medico);
                    }
                }
                reader.Close();
                conn.Close();
            }
            List_Retorno.Sort((p, q) => string.Compare(p.Medico, q.Medico));
            return Task.FromResult(List_Retorno.ToArray());
        }

        public string GetMedicoTxt(int id_medico)
        {
            string tmp_retorno = "";
            // define connection string and query
            //"Data Source=ADMINISTRADOR-P\\SQLEXPRESS;Initial Catalog=Empleados;Integrated Security=True";
            string query = "SELECT * FROM Medicos_derivadores WHERE id_medico_derivador = @Id_medico_derivador";
            //int rowsAffected = 0;
            List<EspecialidadesDerivadoresClass> List_Retorno = new List<EspecialidadesDerivadoresClass>();
            // create connection and command in "using" blocks
            using (NpgsqlConnection conn = Conexion.Crear_conexion())
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.Add(new NpgsqlParameter("@Id_medico_derivador", id_medico));
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

using Npgsql;
using System;
using System.Data;

namespace Estadisticas.Dao
{
    public class conexion
    {
        //procedimiento que abre la conexion sqlsever
        /*private static String Config()
        {
            string path = Application.StartupPath;
            String Servidor;
            String Puerto;
            String Catalogo;
            String line = "";
            String[] tmp = new string[3];
            //System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Asistencia\Asistencia.cfg");
            if (File.Exists(@path.Trim() + "\\recetas.cfg"))
            {
                string[] rows = File.ReadAllLines(@path.Trim() + "\\recetas.cfg", Encoding.Default);
                for (int i = 0; i < rows.Count(); i++)
                {
                    tmp[i] = rows[i];
                }
                Servidor = tmp[0];
                Puerto = tmp[1];
                Catalogo = tmp[2];
                line = "Server=" + Servidor + ";Port=" + Puerto + ";Database=" + Catalogo + ";User Id=postgres;Password=elcapo9242;";
                Program.DataBase = Catalogo;
            }
            else
            {
                line = "Error";
            }
            return line;
        }*/

        public NpgsqlConnection Crear_conexion()
        {
            NpgsqlConnection sql_conexion = null;
            String config = "Server=201.180.55.57;Port=15432;Database=estadisticas_clinica;User Id=sis-clinica;Password=ElCapo1072;"; // "Server=" + Program.properties.servidor + ";Port=" + Program.properties.puerto + ";Database=" + Program.properties.catalogo + ";User Id=postgres;Password=elcapo9242; Timeout=10;"; //Config();
            if (!config.Equals("Error"))
            {
                sql_conexion = new NpgsqlConnection(config);
                try
                {
                    if (sql_conexion.State == ConnectionState.Closed)
                    {
                        sql_conexion.Open();
                        String strSetDatestyle = "SET datestyle = 'ISO, DMY'";

                        using (var cmd = new NpgsqlCommand(strSetDatestyle, sql_conexion))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    sql_conexion.Close();
                    sql_conexion = null;
                }
            }
            return sql_conexion;
        }
    }
}

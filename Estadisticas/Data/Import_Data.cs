using Estadisticas.Dao;
using Estadisticas.Model;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Estadisticas.Data
{
    public class Import_Data
    {
        private static DataTable dt = new DataTable();
        public Boolean Importar(string Archivo)
        {
            Boolean retorno = false;
            try
            {
                AmbClass ambClass = new AmbClass();
                dt = ObjectToData(ambClass);
                int i = 1;
                dt.Rows.Add();
                foreach (string cell in Archivo.Split(','))
                {
                    if (cell.Trim() != "")
                    {
                        switch (PrintAllFields(dt.Columns[i]))
                        {
                            case 1:
                                if (cell.Contains('.'))
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = int.Parse(cell.Substring(0, cell.IndexOf('.'))); //Contains decimal separator
                                }
                                else
                                {
                                    if (cell.Contains('M'))
                                    {
                                        //dt.Rows[dt.Rows.Count - 1][i] = int.Parse(cell); //Contains only numbers, no decimal separator.
                                    }
                                    else
                                    {
                                        dt.Rows[dt.Rows.Count - 1][i] = int.Parse(cell); //Contains only numbers, no decimal separator.
                                    }
                                }
                                break;
                            case 2:
                                if (cell.Length > 10)
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = Convert.ToDateTime(cell);
                                }
                                else
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = DateTime.ParseExact(cell, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                }
                                break;
                            case 3:
                                if (cell.Trim().ToUpper() == "S")
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = true;
                                }
                                else
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = false;
                                }
                                break;
                            case 4:
                                dt.Rows[dt.Rows.Count - 1][i] = cell.Trim();
                                if (i == 45)
                                {
                                    DaoMedicosDerivadores daoMedicoDerivador = new DaoMedicosDerivadores();
                                    if (!daoMedicoDerivador.Check_MedicoDerivador(cell.Trim()))
                                    {
                                        MedicosDerivadoresClass Medico = new MedicosDerivadoresClass();
                                        Medico.Medico = cell.Trim();
                                        Medico.Matricula = " ";
                                        daoMedicoDerivador.Insert_MedicoDerivador(Medico);
                                    }
                                }
                                if (i == 34)
                                {
                                    DaoEspecialidadesDerivadores daoEspecialidadDerivador = new DaoEspecialidadesDerivadores();
                                    DaoEspecialidadesEfector daoEspecialidadEfector = new DaoEspecialidadesEfector();
                                    if (!daoEspecialidadDerivador.Check_EspecialidadDerivador(cell.Trim()))
                                    {
                                        EspecialidadesDerivadoresClass especialidades = new EspecialidadesDerivadoresClass();
                                        especialidades.Especialidad = cell.Trim();                                        
                                        daoEspecialidadDerivador.Insert_EspecialidadDerivador(especialidades);
                                    }
                                    if (!daoEspecialidadEfector.Check_EspecialidadEfector(cell.Trim()))
                                    {
                                        EspecialidadesEfectoresClass especialidades_efector = new EspecialidadesEfectoresClass();
                                        especialidades_efector.Especialidad_Efector = cell.Trim();
                                        daoEspecialidadEfector.Insert_EspecialidadEfector(especialidades_efector);
                                    }
                                }
                                break;
                            case 5:
                                TimeSpan ts1 = TimeSpan.Parse(cell);
                                dt.Rows[dt.Rows.Count - 1][i] = ts1;
                                break;
                            case 6:
                                dt.Rows[dt.Rows.Count - 1][i] = Convert.ToDouble(cell.Trim());
                                break;
                            default:
                                break;
                        }
                    }
                    i++;
                    //}
                }

                foreach (DataRow datos in dt.Rows)
                {
                    DaoAmbEstadistic daoAmb = new DaoAmbEstadistic();
                    if (datos[1].ToString().Trim() != "0")
                    {
                        daoAmb.Insert_Amb_Sql_Class(datos);
                        retorno = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Exception handling here:  Response.Write("Ex.: " + ex.Message);
            }
            return retorno;
        }

        private Boolean checkInt(string data)
        {
            Boolean retorno = false;
            try
            {
                int tempconvert = 0;
                tempconvert = Convert.ToInt32(data.Trim());
                retorno = true;
            }
            catch
            {

            }
            return retorno;
        }

        public static int PrintAllFields(DataColumn obj)
        {
            int retorno = 0;
            if (obj.DataType.Name == "Int32")
            {
                retorno = 1;
            }
            else if (obj.DataType.Name == "DateTime")
            {
                retorno = 2;
            }
            else if (obj.DataType.Name == "Boolean")
            {
                retorno = 3;
            }
            else if (obj.DataType.Name == "String")
            {
                retorno = 4;
            }
            else if (obj.DataType.Name == "TimeSpan")
            {
                retorno = 5;
            }
            else if (obj.DataType.Name == "Float")
            {
                retorno = 6;
            }
            return retorno;
        }

        public static DataTable ObjectToData(object o)
        {
            DataTable dt = new DataTable("OutputData");

            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);

            o.GetType().GetProperties().ToList().ForEach(f =>
            {
                try
                {
                    f.GetValue(o, null);
                    dt.Columns.Add(f.Name, f.PropertyType);
                    dt.Rows[0][f.Name] = f.GetValue(o, null);
                }
                catch { }
            });
            return dt;
        }
    }
}

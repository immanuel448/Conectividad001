using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ConectividadApp.Models
{
    public class MiTablaRepositorio
    {
        //mètodo exclusivo para seleccionar datos de la BD
        public List<GuardarDatosmiTabla> Seleccionar(string datosBD, string instruccion, SqlParameter[] parametros)
        {
            var resultados = new List<GuardarDatosmiTabla>(); 
            try
            {
                using (SqlConnection conexion = new SqlConnection(datosBD))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(instruccion, conexion))
                    {
                        comando.Parameters.AddRange(parametros);

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int identificador = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                    string nombre = reader.IsDBNull(1) ? "vacío" : reader.GetString(1);
                                    int edad = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                    bool activo = reader.IsDBNull(3) ? false : reader.GetBoolean(3);

                                    resultados.Add(new GuardarDatosmiTabla(identificador, nombre, edad, activo));
                                }
                            }
                            else
                            {
                                resultados.Add(new GuardarDatosmiTabla(0, "", 0, false, "No se encontraron datos."));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultados.Add(new GuardarDatosmiTabla(0, "", 0, false, $"Error: {ex.Message}"));
            }

            return resultados;
        }

        //mètodo comùn para Create, Update y Delete
        public string Modificar_guardar(string accion, string datosBD, string instruccion, SqlParameter[] parametros)
        {
            string descripcion = null;

            try
            {
                using (SqlConnection conexion = new SqlConnection(datosBD))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(instruccion, conexion))
                    {
                        comando.Parameters.AddRange(parametros);

                        int resultadoConsulta = comando.ExecuteNonQuery();

                        return resultadoConsulta > 0 ? $"Éxito al {accion}": $"No realizó ninguna acción al intentar {accion}";
                    }
                }
            }
            catch (Exception ex)
            {
                descripcion = $"Error: {ex.Message}";
            }
            return descripcion;
        }
    }
}
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conectividad001
{
    public class GuardarDatos
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public bool Activo { get; set; }
        public string Errores { get; set; }

        public GuardarDatos(string Nombre, int Edad, bool Activo, string Errores = null)
        {
            this.Nombre = Nombre;
            this.Edad = Edad;
            this.Activo = Activo;
            this.Errores = Errores;
        }
    }

    public class miTablaRepositorio
    {
        public List<GuardarDatos> Seleccionar(string datosBD, string instruccion, SqlParameter[] parametros)
        {
            string accion = "Seleccionar";

            var resultados = new List<GuardarDatos>();
            try
            {
                //se establece la conexión con la BD
                using (SqlConnection conexion = new SqlConnection(datosBD))
                {
                    conexion.Open();

                    //se manda el comando
                    using (SqlCommand comando = new SqlCommand(instruccion, conexion))
                    {
                        //se cargan los parámetros
                        comando.Parameters.AddRange(parametros);

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                int contar = 1;
                                while (reader.Read())
                                {
                                    //se optienen los datos
                                    string nombre = reader.IsDBNull(1) ? "vacío" : reader.GetString(1);
                                    int edad = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                    bool activo = reader.IsDBNull(3) ? false : reader.GetBoolean(3);

                                    resultados.Add(new GuardarDatos(nombre, edad, activo));
                                }
                                return resultados;
                            }
                            else
                            {
                                resultados.Add(new GuardarDatos("", 0, false, $"No se encontraron datos al {accion}"));
                                return resultados;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultados.Add(new GuardarDatos("", 0, false, $"Error al {accion}: {ex.Message}"));

                return resultados;
            }
        }
    }
}

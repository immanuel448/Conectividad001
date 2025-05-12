using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ConectividadApp.Models
{
{
    public class MiTablaRepositorio
    {
        private static string accion {  get; set; }

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
                                    string nombre = reader.IsDBNull(1) ? "vacío" : reader.GetString(1);
                                    int edad = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                    bool activo = reader.IsDBNull(3) ? false : reader.GetBoolean(3);

                                    resultados.Add(new GuardarDatosmiTabla(nombre, edad, activo));
                                }
                            }
                            else
                            {
                                resultados.Add(new GuardarDatosmiTabla("", 0, false, "No se encontraron datos."));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultados.Add(new GuardarDatosmiTabla("", 0, false, $"Error: {ex.Message}"));
            }

            return resultados;
        }

        private static void Insertar(string datosBD)
        {
            accion = "Insertar";
            try
            {
                //se establece la conexión con la BD
                using (SqlConnection conexion = new SqlConnection(datosBD))
                {
                    string instruccion = "INSERT INTO miTabla (nombre, edad, activo) VALUES (@nombre, @edad, @activo)";
                    conexion.Open();

                    //se manda el comando
                    using (SqlCommand comando = new SqlCommand(instruccion, conexion))
                    {
                        //se cargan los parámetros
                        comando.Parameters.AddWithValue("@nombre", "Fulanito");
                        comando.Parameters.AddWithValue("@edad", 33);
                        comando.Parameters.AddWithValue("@activo", true);

                        int filasAfectadas = comando.ExecuteNonQuery();

                        //operador ternario, para anunciar si realizó o no la acción
                        Console.WriteLine(filasAfectadas > 0
                            ? $"Éxito al {accion}"
                            : $"Fracaso al {accion}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al {accion}: {ex.Message}");
            }
        }

        private static void Actualizar(string datosBD)
        {
            accion = "Actualizar";
            try
            {
                //se establece la conexión con la BD
                using (SqlConnection conexion = new SqlConnection(datosBD))
                {
                    string instruccion = "UPDATE miTabla SET nombre = @nombre, edad = @edad WHERE  id = @id";
                    conexion.Open();

                    //se manda el comando
                    using (SqlCommand comando = new SqlCommand(instruccion, conexion))
                    {
                        //se cargan los parámetros
                        comando.Parameters.AddWithValue("@nombre", "Para borrar");
                        comando.Parameters.AddWithValue("@edad", 10);
                        comando.Parameters.AddWithValue("@id", 5);

                        int filasAfectadas = comando.ExecuteNonQuery();

                        //operador ternario, para anunciar si realizó o no la acción
                        Console.WriteLine(filasAfectadas > 0
                            ? $"Éxito al {accion}"
                            : $"Fracaso al {accion}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al {accion}: {ex.Message}");
            }
        }

        private static void Borrar(string datosBD)
        {
            accion = "Borrar";
            try
            {
                //se establece la conexión con la BD
                using (SqlConnection conexion = new SqlConnection(datosBD))
                {
                    string instruccion = "DELETE FROM miTabla WHERE  id = @id";
                    conexion.Open();

                    //se manda el comando
                    using (SqlCommand comando = new SqlCommand(instruccion, conexion))
                    {
                        //se cargan los parámetros
                        comando.Parameters.AddWithValue("@id", 5);

                        int filasAfectadas = comando.ExecuteNonQuery();

                        //operador ternario, para anunciar si realizó o no la acción
                        Console.WriteLine(filasAfectadas > 0
                            ? $"Éxito al {accion}"
                            : $"Fracaso al {accion}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al {accion}: {ex.Message}");
            }
        }
    }
}
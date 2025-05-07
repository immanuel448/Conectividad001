using Conectividad001;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Conectividad
{
    public class Program
    {
        private static string accion { get; set; }

        public static void Main(string [] args)
        {
            string datosBD = "Server=.;Database=miBD;Trusted_Connection=true;TrustServerCertificate=true;";
            Console.WriteLine("e0001");
            
            //Seleccionar(datosBD);

            var resultados = Seleccionar(datosBD);
            int contar = 1;
            foreach (var item in resultados)
            {
                if (item.Descripcion == null)
                {
                    //sin descripción
                    Console.WriteLine($"\n-------------------- \n El dato número {contar++} es:\n Nombre: {item.Nombre},\n Edad: {item.Edad},\n Activo: {item.Activo}");
                }
                else
                {
                    Console.WriteLine("!!!" + item.Descripcion);
                }
            }
        }


        private static List<MiTablaRepositorio> Seleccionar(string datosBD)
        {
            accion = "Seleccionar";
            var resultados = new List<MiTablaRepositorio>();
            Console.WriteLine("e0002");
            try
            {
                //se establece la conexión con la BD
                using (SqlConnection conexion = new SqlConnection(datosBD))
                {
                    string instruccion = "SELECT * FROM miTabla WHERE id IN (@id01, @id02, @id03)";
                    conexion.Open();

                    //se manda el comando
                    using (SqlCommand comando = new SqlCommand(instruccion, conexion))
                    {
                        //se cargan los parámetros
                        comando.Parameters.AddWithValue("@id01", 1);
                        comando.Parameters.AddWithValue("@id02", 2);
                        comando.Parameters.AddWithValue("@id03", 3);

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            //creo que hay errro en la segunda parte
                            if (reader.HasRows)
                            {
                                int contar = 1;
                                while (reader.Read())
                                {
                                    string nombre = reader.IsDBNull(1) ? "vacío" : reader.GetString(1);
                                    int edad = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                    bool activo = reader.IsDBNull(3) ? false : reader.GetBoolean(3);

                                    //Console.WriteLine($"\n-------------------- \n El dato número {contar++} es:\n Nombre: {nombre},\n Edad: {edad},\n Activo: {activo}");

                                    resultados.Add(new MiTablaRepositorio(nombre, edad, activo));
                                }
                                return resultados;
                            }
                            else
                            {
                                //Console.WriteLine($"No se encontraron datos al {accion}");
                                resultados.Add(new MiTablaRepositorio("", 0, false, $"No se encontraron datos al {accion}"));
                                return resultados;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error al {accion}: {ex.Message}");
                resultados.Add(new MiTablaRepositorio("", 0, false, $"Error al {accion}: {ex.Message}"));

                return resultados;
            }
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

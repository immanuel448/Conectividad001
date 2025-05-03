using Microsoft.Data.SqlClient;
using System.Data;

namespace Conectividad
{
    public class Program
    {
        private static string accion;

        public static void Main(string [] args)
        {
            string datosBD = "Server=.;Database=miBD;Trusted_Connection=true;TrustServerCertificate=true";
            Seleccionar(datosBD);
        }

        private static void Seleccionar(string datosBD)
        {
            accion = "Seleccionar";
            try
            {
                //se establece la conexión con la BD
                using (SqlConnection conexion = new SqlConnection(datosBD))
                {
                    string instruccion = "SELECT * FROM miTabla WHERE id IN (@id01, @id02)";
                    conexion.Open();

                    //se manda el comando
                    using (SqlCommand comando = new SqlCommand(instruccion, conexion))
                    {
                        //se cargan los parámetros
                        comando.Parameters.AddWithValue("@id01", 5);
                        comando.Parameters.AddWithValue("@id02", 7);

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            //creo que hay errro en la segunda parte
                            if (reader.HasRows)
                            {
                                int contar = 1;
                                while (reader.Read())
                                {
                                    string nombre = reader.IsDBNull(1)? "vacío": reader.GetString(1);
                                    int edad= reader.IsDBNull(2)? 0: reader.GetInt32(2);
                                    bool activo= reader.IsDBNull(3)? false: reader.GetBoolean(3);
                                    Console.WriteLine($"-------------------- \n El dato número {contar++} es:\n Nombre: {nombre},\n Edad: {edad},\n Activo: {activo}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"No se encontraron datos al {accion}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al {accion}: {ex.Message}");
            }
        }

        private static void Insertar (string datosBD)
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
                        if (filasAfectadas > 0)
                        {
                            //se realizó la consulta
                            Console.WriteLine($"Éxito al {accion}");
                        }
                        else
                        {
                            //no hizo la consulta
                            Console.WriteLine($"Fracaso al {accion}");
                        }
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
                        comando.Parameters.AddWithValue("@nombre", "Cambiado");
                        comando.Parameters.AddWithValue("@edad", 10);
                        comando.Parameters.AddWithValue("@id", 6);

                        int filasAfectadas = comando.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                        {
                            //se realizó la consulta
                            Console.WriteLine($"Éxito al {accion}");
                        }
                        else
                        {
                            //no hizo la consulta
                            Console.WriteLine($"Fracaso al {accion}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al {accion}: {ex.Message}");

            }
        }

        private static void Delete(string datosBD)
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
                        comando.Parameters.AddWithValue("@id", 3);

                        int filasAfectadas = comando.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                        {
                            //se realizó la consulta
                            Console.WriteLine($"Éxito al {accion}");
                        }
                        else
                        {
                            //no hizo la consulta
                            Console.WriteLine($"Fracaso al {accion}");
                        }
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

using Conectividad001;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.Metrics;

namespace Conectividad
{
    public class Program
    {
        private static string accion { get; set; }

        public static void Main(string [] args)
        {
            const string datosBD = "Server=.;Database=miBD;Trusted_Connection=true;TrustServerCertificate=true;";
            Console.WriteLine("e0001");
            
            Seleccionar(datosBD);
        }


        private static void Seleccionar(string datosBD)
        {
            //instruccion
            string instruccion = "SELECT * FROM miTabla WHERE id IN (@id01, @id02, @id03)";

            //datos a pasar
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id01", 1),
                new SqlParameter("@id02", 2),
                new SqlParameter("@id03", 3)
            };

            //creo el objeto
            var repo = new miTablaRepository();

            //y selecciono el metodo de este objeto
            var consultaSeleccionar = repo.Seleccionar(datosBD, instruccion, parametros);

            int contar = 1;
            //para mostrar los datos
            foreach (var item in consultaSeleccionar)
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

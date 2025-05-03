using Microsoft.Data.SqlClient;
using System.Data;

namespace Conectividad
{
    public class Program
    {
        private static string accion { get; set; }

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
    }
}

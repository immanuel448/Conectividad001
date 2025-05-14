
using ConectividadApp.Controllers;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.Metrics;

namespace ConectividadApp
{
    public class Program
    {
        //se crea un objeto del controlador
        private static MiTablaController controller = new MiTablaController();

        public static void Main(string[] args)
        {
            string datosBD = "Server=.;Database=miBD;Trusted_Connection=true;TrustServerCertificate=true;";

            Seleccionar(datosBD);
            //Insertar(datosBD);

        }

        private static void Seleccionar(string CadenaConexion)
        {
            //se usa un método del objeto creado, devuelve un objeto que contiene los resultados
            var resultados = controller.SeleccionarDatos(CadenaConexion);
            int contar = 1;
            foreach (var item in resultados)
            {
                if (item.Errores == null)
                {
                    Console.WriteLine($"\n-------------------- \n El dato número {contar++} es:\n Nombre: {item.Nombre},\n Edad: {item.Edad},\n Activo: {item.Activo}");
                }
                else
                {
                    Console.WriteLine("!!! " + item.Errores);
                }
            }
        }

        private static void Insertar(string CadenaConexion)
        {
            //se usa un método del objeto creado, devuelve un objeto que contiene los resultados
            var resultados = controller.InsertarDatos(CadenaConexion);
            int contar = 1;
            Console.WriteLine(resultados);
        }

        private static void Actualizar(string CadenaConexion)
        {
            //se usa un método del objeto creado, devuelve un objeto que contiene los resultados
            var resultados = controller.ActualizarDatos(CadenaConexion);
            int contar = 1;
            Console.WriteLine(resultados);
        }

        private static void Borrar(string CadenaConexion)
        {
            //se usa un método del objeto creado, devuelve un objeto que contiene los resultados
            var resultados = controller.BorrarrDatos(CadenaConexion);
            int contar = 1;
            Console.WriteLine(resultados);
        }
    }
}




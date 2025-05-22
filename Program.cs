
using ConectividadApp.Controllers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
            // Construir configuración para leer appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration configuration = builder.Build();

            // Obtener cadena de conexión desde el archivo
            string datosBD = configuration.GetConnectionString("MiConexionSQL");

            //Seleccionar(datosBD);
            for (int contar = 30; contar < 35; contar++)
            {
                Insertar(datosBD, $"e00{contar}");
            }
        }

        private static void Seleccionar(string CadenaConexion)
        {
            var idsParaBuscar = new List<int>() { 1,2,3,4 };
            //se usa un método del objeto creado, devuelve un objeto que contiene los resultados
            var resultados = controller.SeleccionarDatos(CadenaConexion, idsParaBuscar);
            int contar = 1;
            //con esto se presentan los resultados
            foreach (var item in resultados)
            {
                if (item.Errores == null)
                {
                    Console.WriteLine($"\n-------------------- \n El dato número {contar++} es:\n ID: {item.Identificador} \n Nombre: {item.Nombre},\n Edad: {item.Edad},\n Activo: {item.Activo}");
                }
                else
                {
                    Console.WriteLine("!!! " + item.Errores);
                }
            }
        }

        private static void Insertar(string CadenaConexion, string nombreDato)
        {
            //devuelve un string
            var resultados = controller.InsertarDatos(CadenaConexion, nombreDato, 15, false);
            Console.WriteLine(resultados);
        }

        private static void Actualizar(string CadenaConexion)
        {
            //devuelve un string
            var resultados = controller.ActualizarDatos(CadenaConexion, "jeje", 50 , 15);
            Console.WriteLine(resultados);
        }

        private static void Borrar(string CadenaConexion)
        {
            //devuelve un string
            var resultados = controller.BorrarDatos(CadenaConexion, 5);
            Console.WriteLine(resultados);
        }
    }
}




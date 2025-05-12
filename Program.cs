
using ConectividadApp.Controllers;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.Metrics;

namespace ConectividadApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string datosBD = "Server=.;Database=miBD;Trusted_Connection=true;TrustServerCertificate=true;";

            var controller = new MiTablaController();
            var resultados = controller.ObtenerDatos(datosBD);

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
    }
}




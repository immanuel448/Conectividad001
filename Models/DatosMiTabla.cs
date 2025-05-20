using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectividadApp.Models
{
    public class DatosMiTabla
    {
        //todos los datos de la tabla "miTabla"
        public int Identificador { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public bool Activo { get; set; }
        public string Errores { get; set; }


        public DatosMiTabla(int identificador = 0, string nombre = "", int edad = 0, bool activo = false, string errores = null)
        {
            Identificador = identificador;
            Nombre = nombre;
            Edad = edad;
            Activo = activo;
            Errores = errores;
        }
    }
}


using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectividadApp.Models
{
    public class GuardarDatosmiTabla
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public bool Activo { get; set; }
        public string Errores { get; set; }

        public GuardarDatosmiTabla(string nombre, int edad, bool activo, string errores = null)
        {
            Nombre = nombre;
            Edad = edad;
            Activo = activo;
            Errores = errores;
        }
    }
}


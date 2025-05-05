using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conectividad001
{
    public class MiTablaRepositorio
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public bool Activo { get; set; }
        public string Descripcion { get; set; }

        public MiTablaRepositorio(string Nombre, int Edad, bool Activo, string Descripcion = null)
        {
            this.Nombre = Nombre;
            this.Edad = Edad;
            this.Activo = Activo;
            this.Descripcion = Descripcion;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConectividadApp.Models;
using Microsoft.Data.SqlClient;

namespace ConectividadApp.Controllers
{
    public class MiTablaController
    {
        private readonly MiTablaRepositorio _repositorio;

        public MiTablaController()
        {
            //esta clase realiza la consulta con la bd (seleccionar) y regresa un objeto de GuardarDatosMiTabla
            _repositorio = new MiTablaRepositorio();
        }

        public List<GuardarDatosmiTabla> SeleccionarDatos(string cadenaConexion)
        {
            string instruccion = "SELECT * FROM miTabla WHERE id IN (@id01, @id02, @id03)";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id01", 4),
                new SqlParameter("@id02", 5),
                new SqlParameter("@id03", 6)
            };

            return _repositorio.Seleccionar(cadenaConexion, instruccion, parametros);
        }

        public string InsertarDatos(string cadenaConexion)
        {
            string instruccion = "INSERT INTO miTabla (nombre, edad, activo) VALUES (@nombre, @edad, @activo)";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", "nuevo003"),
                new SqlParameter("@edad", 10),
                new SqlParameter("@activo", false)
        };

            return _repositorio.Modificar_guardar("Insertar", cadenaConexion, instruccion, parametros);
        }
    }
}
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
            _repositorio = new MiTablaRepositorio();
        }

        public List<GuardarDatosmiTabla> ObtenerDatos(string cadenaConexion)
        {
            string instruccion = "SELECT * FROM miTabla WHERE id IN (@id01, @id02, @id03)";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id01", 1),
                new SqlParameter("@id02", 2),
                new SqlParameter("@id03", 3)
            };

            return _repositorio.Seleccionar(cadenaConexion, instruccion, parametros);
        }
    }
}
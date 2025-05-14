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
            string instruccion = "SELECT * FROM miTabla WHERE id != @id";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id", 5),
            };

            return _repositorio.Seleccionar(cadenaConexion, instruccion, parametros);
        }

        public string InsertarDatos(string cadenaConexion)
        {
            string instruccion = "INSERT INTO miTabla (nombre, edad, activo) VALUES (@nombre, @edad, @activo)";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", "nuevo004"),
                new SqlParameter("@edad", 11),
                new SqlParameter("@activo", false)
                };

            return _repositorio.Modificar_guardar("Insertar", cadenaConexion, instruccion, parametros);
        }

        public string ActualizarDatos(string cadenaConexion)
        {
            string instruccion = "UPDATE miTabla SET nombre = @nombre, edad = @edad WHERE id = @id";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", "Eeeeeeeeeeeee"),
                new SqlParameter("@edad", 10),
                new SqlParameter("@id", 8)
            }; 

            return _repositorio.Modificar_guardar("Actualizar", cadenaConexion, instruccion, parametros);
        }

        public string BorrarrDatos(string cadenaConexion)
        {
            string instruccion = "DELETE FROM miTabla WHERE  id = @id";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id", 8)
            };

            return _repositorio.Modificar_guardar("Borrar", cadenaConexion, instruccion, parametros);
        }
    }
}
using System;
using System.CodeDom.Compiler;
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




        // Método auxiliar para crear los parámetros SQL
        private SqlParameter[] CrearParametros(string[] nombresParametros, object[] valores)
        {
            if (nombresParametros.Length != valores.Length)
                throw new ArgumentException("El número de nombres de parámetros no coincide con el número de valores.");

            var parametros = new SqlParameter[nombresParametros.Length];
            for (int i = 0; i < nombresParametros.Length; i++)
            {
                parametros[i] = new SqlParameter(nombresParametros[i], valores[i]);
            }
            return parametros;
        }

        // Método que ejecuta Insertar, Actualizar o Borrar dependiendo de la operación
        private string EjecutarOperacion(string operacion, string cadenaConexion, string instruccion, string[] nombresParametros, object[] valores)
        {
            var parametros = CrearParametros(nombresParametros, valores);
            return _repositorio.Modificar_guardar(operacion, cadenaConexion, instruccion, parametros);
        }





        public List<GuardarDatosmiTabla> SeleccionarDatos(string cadenaConexion, List<int> identificadores)
        {
            if (identificadores == null || identificadores.Count == 0)
            {
                //si no se pasaron datos para buscar en la bd, se termina este método
                return new List<GuardarDatosmiTabla>();
            }

            //para contener los parámetros "SQL"
            var parametros = new List<SqlParameter>();
            //lista de los nombres de los parámetros "string"
            var nombresParametros = new List<string>();

            //aquí se forman los parámetros para mandarlos (nombre y parámetro "SQL")
            for (int i = 0; i < identificadores.Count; i++)
            {
                string estructuraNombre = "@id" + i;
                nombresParametros.Add(estructuraNombre);
                parametros.Add(new SqlParameter(estructuraNombre, identificadores[i]));
            }

            string juntarNombres = string.Join(", ", nombresParametros);

            string instruccion = $"SELECT * FROM miTabla WHERE id in ({juntarNombres})";

            return _repositorio.Seleccionar(cadenaConexion, instruccion, parametros.ToArray());
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

    /*
     using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ConectividadApp.Models;

namespace ConectividadApp.Controllers
{
    public class MiTablaController
    {
        private readonly MiTablaRepositorio _repositorio;

        // Constructor de la clase
        public MiTablaController()
        {
            _repositorio = new MiTablaRepositorio();
        }

        // Método auxiliar para crear los parámetros SQL
        private SqlParameter[] CrearParametros(string[] nombresParametros, object[] valores)
        {
            if (nombresParametros.Length != valores.Length)
                throw new ArgumentException("El número de nombres de parámetros no coincide con el número de valores.");

            var parametros = new SqlParameter[nombresParametros.Length];
            for (int i = 0; i < nombresParametros.Length; i++)
            {
                parametros[i] = new SqlParameter(nombresParametros[i], valores[i]);
            }
            return parametros;
        }

        // Método que ejecuta Insertar, Actualizar o Borrar dependiendo de la operación
        private string EjecutarOperacion(string operacion, string cadenaConexion, string instruccion, string[] nombresParametros, object[] valores)
        {
            var parametros = CrearParametros(nombresParametros, valores);
            return _repositorio.Modificar_guardar(operacion, cadenaConexion, instruccion, parametros);
        }

        // Seleccionar Datos
        public List<GuardarDatosmiTabla> SeleccionarDatos(string cadenaConexion, List<int> identificadores)
        {
            if (identificadores == null || identificadores.Count == 0)
            {
                return new List<GuardarDatosmiTabla>();
            }

            // Crear parámetros
            var nombresParametros = identificadores.Select(i => "@id" + i).ToList();
            var parametros = identificadores.Cast<object>().ToArray();

            // Crear instrucción SQL
            string instruccion = $"SELECT * FROM miTabla WHERE id IN ({string.Join(", ", nombresParametros)})";

            return _repositorio.Seleccionar(cadenaConexion, instruccion, CrearParametros(nombresParametros.ToArray(), parametros));
        }

        // Insertar Datos
        public string InsertarDatos(string cadenaConexion)
        {
            string instruccion = "INSERT INTO miTabla (nombre, edad, activo) VALUES (@nombre, @edad, @activo)";

            string[] nombresParametros = { "@nombre", "@edad", "@activo" };
            object[] valores = { "nuevo004", 11, false };

            return EjecutarOperacion("Insertar", cadenaConexion, instruccion, nombresParametros, valores);
        }

        // Actualizar Datos
        public string ActualizarDatos(string cadenaConexion)
        {
            string instruccion = "UPDATE miTabla SET nombre = @nombre, edad = @edad WHERE id = @id";

            string[] nombresParametros = { "@nombre", "@edad", "@id" };
            object[] valores = { "Eeeeeeeeeeeee", 10, 8 };

            return EjecutarOperacion("Actualizar", cadenaConexion, instruccion, nombresParametros, valores);
        }

        // Borrar Datos
        public string BorrarDatos(string cadenaConexion)
        {
            string instruccion = "DELETE FROM miTabla WHERE id = @id";

            string[] nombresParametros = { "@id" };
            object[] valores = { 8 };

            return EjecutarOperacion("Borrar", cadenaConexion, instruccion, nombresParametros, valores);
        }
    }
}
*/
}
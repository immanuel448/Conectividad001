using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using ConectividadApp.Models;
using Microsoft.Data.SqlClient;


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

        // Método auxiliar para crear un array de objetos SqlParameter
        private SqlParameter[] CrearParametros(string[] nombresParametros, object[] valores)
        {
            if (nombresParametros.Length != valores.Length)
                throw new ArgumentException("El número de nombres de parámetros no coincide con el número de valores.");

            //en este array se van a colocar objetos de SqlParameter
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


        // Seleccionar Datos, aquí no se usa el método "EjecutarOperacion"
        public List<GuardarDatosmiTabla> SeleccionarDatos(string cadenaConexion, List<int> identificadores)
        {
            if (identificadores == null || identificadores.Count == 0)
            {
                return new List<GuardarDatosmiTabla>();
            }

            // Crear parámetros
            var nombresParametros = identificadores.Select(i => "@id" + i).ToList();
            //aquí se hace uso de LINQ, hay que pasar un array de objet
            var parametros = identificadores.Cast<object>().ToArray();

            // Crear instrucción SQL, "string.Join" une todos los elementos de la lista
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


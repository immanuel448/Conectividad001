﻿using System;
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

        // Método auxiliar devuelve un array de objetos SqlParameter
        private SqlParameter[] CrearParametros(string[] nombresParametros, object[] valores)
        {
            if (nombresParametros.Length != valores.Length)
                throw new ArgumentException("El número de nombres de parámetros no coincide con el número de valores.");

            //en este array se van a colocar objets de SqlParameter
            var parametros = new SqlParameter[nombresParametros.Length];
            for (int i = 0; i < nombresParametros.Length; i++)
            {
                //la estructura es "new SqlParameter(@nombre", "El nombre");"
                parametros[i] = new SqlParameter(nombresParametros[i], valores[i]);
            }
            return parametros;
        }

        // Método que ejecuta Insertar, Actualizar o Borrar dependiendo de la operación
        private string EjecutarOperacion(string nombreOperacion, string cadenaConexion, string instruccion, string[] nombresParametros, object[] valores)
        {
            try
            {
                var parametros = CrearParametros(nombresParametros, valores);
                return _repositorio.Modificar_guardar(nombreOperacion, cadenaConexion, instruccion, parametros);
            }
            catch (Exception ex)
            {
                return $"Error en la operación {nombreOperacion}: {ex.Message}";
            }
        }


        // Seleccionar Datos, aquí no se usa el método "EjecutarOperacion"
        public List<DatosMiTabla> SeleccionarDatos(string cadenaConexion, List<int> identificadores)
        {
            try
            {
                //si no hay parametros, se termina
                if (identificadores == null || identificadores.Count == 0)
                {
                    return new List<DatosMiTabla>();
                }

                // LINQ, Crea nombre de los parámetros estructura "@id..."
                var nombresParametros = identificadores.Select(i => "@id" + i).ToList();
                //aquí se hace uso de LINQ,se ocupa pasar un array de objets
                var valores = identificadores.Cast<object>().ToArray();

                // Crear instrucción SQL, "string.Join" une todos los elementos de la lista
                string instruccion = $"SELECT * FROM miTabla WHERE id IN ({string.Join(", ", nombresParametros)})";

                return _repositorio.Seleccionar(cadenaConexion, instruccion, CrearParametros(nombresParametros.ToArray(), valores));

            }
            catch (Exception ex)
            {
                var resultados = new List<DatosMiTabla>();
                resultados.Add(new DatosMiTabla(errores: $"Error al seleccionar {ex.Message}"));
                return resultados;
            }
        }

        // Insertar Datos
        public string InsertarDatos(string cadenaConexion, string nombre, int edad, bool activo)
        {
            string instruccion = "INSERT INTO miTabla (nombre, edad, activo) VALUES (@nombre, @edad, @activo)";

            string[] nombresParametros = { "@nombre", "@edad", "@activo" };
            object[] valores = { nombre, edad, activo};

            return EjecutarOperacion("Insertar", cadenaConexion, instruccion, nombresParametros, valores);
        }

        // Actualizar Datos
        public string ActualizarDatos(string cadenaConexion, string nombre, int edad, int id)
        {
            string instruccion = "UPDATE miTabla SET nombre = @nombre, edad = @edad WHERE id = @id";

            string[] nombresParametros = { "@nombre", "@edad", "@id" };
            object[] valores = {nombre, edad, id };

            return EjecutarOperacion("Actualizar", cadenaConexion, instruccion, nombresParametros, valores);
        }

        // Borrar Datos
        public string BorrarDatos(string cadenaConexion, int id)
        {
            string instruccion = "DELETE FROM miTabla WHERE id = @id";

            string[] nombresParametros = { "@id" };
            object[] valores = { id };

            return EjecutarOperacion("Borrar", cadenaConexion, instruccion, nombresParametros, valores);
        }
    }
}


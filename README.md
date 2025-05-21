# Conexión a Base de Datos SQL Server con C#

Este proyecto es un ejemplo sencillo de cómo establecer una conexión a una base de datos **SQL Server** utilizando **C#**, siguiendo buenas prácticas de organización del código.

## Estructura del Proyecto

El código está organizado en dos capas principales:

- **Modelo:** Contiene las clases relacionadas con la lógica de acceso a datos, como la gestión de la conexión y ejecución de consultas SQL.
- **Controlador:** Encargado de coordinar las operaciones de la aplicación y servir de intermediario entre el modelo y cualquier posible interfaz futura (como una vista o API).

> 🔧 Este proyecto **no incluye una vista**, ya que el enfoque está en la lógica backend y la estructura limpia del código.

## Tecnologías Utilizadas

- Lenguaje: C# (.NET)
- Base de datos: SQL Server
- Arquitectura: Separación de responsabilidades (Modelo / Controlador)

## Objetivo

El objetivo de este repositorio es mostrar una implementación clara y concisa de cómo:

- Establecer una conexión a SQL Server
- Ejecutar consultas básicas (ej. SELECT, INSERT)
- Organizar el código para facilitar su mantenimiento y escalabilidad

## Uso

1. Configura tu cadena de conexión en el archivo correspondiente del modelo.
2. Ejecuta el proyecto desde un entorno compatible con C# (como Visual Studio).
3. Observa cómo se realiza la conexión y manipulación de los datos.

## Contribuciones

Este es un proyecto base, abierto a mejoras como:

- Incorporación de una capa de vistas o una API REST
- Implementación de repositorios y patrones como DAO o Repository
- Manejo avanzado de errores y logs

---

¡Gracias por visitar este repositorio!

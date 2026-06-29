# Trabajo de Clase - Clase 22 - C# y MySQL

## BuscaBDAlumnos

Aplicacion de consola en C# que busca un alumno en una base de datos MySQL a partir de su legajo.

El proyecto fue creado tomando como base el ejemplo `ListaBDAlumnos`, manteniendo la misma forma de trabajo vista en clase:

- Proyecto de consola en .NET.
- Paquete `MySql.Data`.
- Conexion lineal a MySQL.
- Uso de `using` para cerrar conexion, comando y lector.
- Manejo de errores con `try...catch`.
- Consulta parametrizada para evitar concatenar valores en SQL.

## Ubicacion del proyecto

```text
Clase22_CSMySQL/BuscaBDAlumnos
```

## Requisitos

- .NET SDK 10.
- XAMPP con MySQL iniciado.
- Base de datos `prog3n3` creada a partir del archivo SQL de la clase.

La cadena de conexion usada por el proyecto es:

```csharp
Server=127.0.0.1;Port=3306;Database=prog3n3;Uid=root;Pwd=;
```

Esta configuracion corresponde a XAMPP usando el usuario `root` sin contrasena.

## Como ejecutar

Desde la terminal de VS Code:

```powershell
cd .\Clase22_CSMySQL\BuscaBDAlumnos
& "C:\Program Files\dotnet\dotnet.exe" run
```

Luego ingresar un legajo existente, por ejemplo:

```text
30154879
```

## Funcionamiento

El programa pide al usuario que ingrese un legajo.

Si el dato ingresado no es un numero entero, muestra un mensaje de error y finaliza sin abrir la conexion a la base de datos.

Si el dato es valido, abre la conexion a MySQL y ejecuta una consulta parametrizada:

```sql
SELECT legajo, nombre, apellido, email, carrera, turno, fecha_inscripcion
FROM alumnos
WHERE legajo = @legajo
```

Si encuentra el alumno, muestra sus datos por consola.

Si no lo encuentra, muestra:

```text
No se encontro un alumno con el legajo ingresado.
```

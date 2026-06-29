/*===============================================================================
PROGRAMACION III Conexion Lineal a MySQL 
 
 Antes de correr el proyecto, se debe instalar el driver de MySQL.
 En VSCode ejecutar este comando por terminal:
 dotnet add package MySql.Data --source https://api.nuget.org/v3/index.json
===============================================================================*/
using System;
// Importamos los componentes del driver de MySQL.
using MySqlConnection = MySql.Data.MySqlClient.MySqlConnection;
using MySqlCommand = MySql.Data.MySqlClient.MySqlCommand;
using MySqlDataReader = MySql.Data.MySqlClient.MySqlDataReader;
using MySqlException = MySql.Data.MySqlClient.MySqlException;

namespace BuscaBDAlumnos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese el legajo del alumno a buscar:");
            string legajoIngresado = Console.ReadLine() ?? "";

            if (!int.TryParse(legajoIngresado, out int legajoBuscado))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El legajo ingresado no es valido. Debe ser un numero entero.");
                Console.ResetColor();
                Console.WriteLine("Presione cualquier tecla para salir...");
                Console.ReadKey();
                return;
            }

            // Cadena de conexion.
            string connectionString = "Server=127.0.0.1;Port=3306;Database=prog3n3;Uid=root;Pwd=;";
            Console.WriteLine("Intentando conectar a la base de datos MySQL...");

            try
            {
                // Abrimos la conexion asegurando el cierre de recursos con 'using'.
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                { //conexion es un OBJETO que prepara el canal TCP para conectar al servidor MySql.
                    conexion.Open(); //Aqui es donde la conexion se abre. (Se cierra gracias a using)

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Conexion exitosa al servidor de MySQL!\n");
                    Console.ResetColor();

                    // Sentencia SQL parametrizada para buscar un unico alumno por legajo.
                    string consulta = "SELECT legajo, nombre, apellido, email, carrera, turno, fecha_inscripcion FROM alumnos WHERE legajo = @legajo";

                    using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@legajo", legajoBuscado);

                        using (MySqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                string legajo = lector["legajo"].ToString() ?? "";
                                string nombre = lector["nombre"].ToString() ?? "";
                                string apellido = lector["apellido"].ToString() ?? "";
                                string email = lector["email"].ToString() ?? "";
                                string carrera = lector["carrera"].ToString() ?? "";
                                string turno = lector["turno"].ToString() ?? "";
                                string fechaInscripcion = lector["fecha_inscripcion"].ToString() ?? "";

                                Console.WriteLine("==========================================");
                                Console.WriteLine("          DATOS DEL ALUMNO");
                                Console.WriteLine("==========================================");
                                Console.WriteLine("Legajo: " + legajo);
                                Console.WriteLine("Nombre: " + nombre);
                                Console.WriteLine("Apellido: " + apellido);
                                Console.WriteLine("Email: " + email);
                                Console.WriteLine("Carrera: " + carrera);
                                Console.WriteLine("Turno: " + turno);
                                Console.WriteLine("Fecha de inscripcion: " + fechaInscripcion);
                                Console.WriteLine("==========================================\n");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("No se encontro un alumno con el legajo ingresado.");
                                Console.ResetColor();
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Control de errores especificos de MySQL.
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ocurrio un error al intentar operar con la base de datos MySQL:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                // Control de errores generales.
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ocurrio un error inesperado:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            Console.WriteLine("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}

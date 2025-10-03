using System;
using Renci.SshNet;

namespace VictorDice { 
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Gestor de Servidor Ubuntu ===");

        // Preguntar si es local o remoto
        bool es_local = Utilidades.PreguntarEsLocal();
        string host = "";
        string usuario = "";
        string contrasena = "";

        if (!es_local)
        {
            // Pedir y validar credenciales
            host = Utilidades.PedirEntrada("Host (ej. 192.168.1.100): ", true);
            usuario = Utilidades.PedirEntrada("Usuario: ", true);
            contrasena = Utilidades.PedirEntrada("Contraseña: ", true);
        }

        // Crear objeto de conexión
        var gestion_conexion = new GestionConexion(host, usuario, contrasena, es_local);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Menú de Gestión ===");
            Console.WriteLine("1. Comprobar actualizaciones de paquetes");
            Console.WriteLine("2. Listar servicios activos");
            Console.WriteLine("3. Monitorear recursos del sistema");
            Console.WriteLine("4. Gestionar Apache2");
            Console.WriteLine("5. Gestionar PostgreSQL");
            Console.WriteLine("6. Salir");
            Console.Write("Selecciona una opción (1-6): ");

            string opcion = Console.ReadLine();

            try
            {
                switch (opcion)
                {
                    case "1":
                        GestionComandos.ComprobarActualizaciones(gestion_conexion);
                        break;
                    case "2":
                        GestionComandos.ListarServiciosActivos(gestion_conexion);
                        break;
                    case "3":
                        GestionComandos.MonitorearRecursos(gestion_conexion);
                        break;
                    case "4":
                        GestionComandos.GestionarApache2(gestion_conexion);
                        break;
                    case "5":
                        GestionComandos.GestionarPostgresql(gestion_conexion);
                        break;
                    case "6":
                        Console.WriteLine("¡Adiós!");
                        return;
                    default:
                        Console.WriteLine("Opción inválida. Presiona Enter para continuar.");
                        Console.ReadLine();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Presiona Enter para continuar.");
                Console.ReadLine();
            }
        }
    }
}
}
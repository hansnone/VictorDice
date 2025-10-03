using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictorDice
{
    using System;
    using System.Threading;

    class GestionComandos
    {
        public static void ComprobarActualizaciones(GestionConexion conexion)
        {
            Console.WriteLine("Comprobando actualizaciones de paquetes...");
            string resultado = conexion.EjecutarComando("sudo apt update && apt list --upgradable");
            Console.WriteLine(resultado);
            Console.WriteLine("Presiona Enter para continuar.");
            Console.ReadLine();
        }

        public static void ListarServiciosActivos(GestionConexion conexion)
        {
            Console.WriteLine("Listando servicios activos...");
            string resultado = conexion.EjecutarComando("systemctl list-units --type=service --state=active");
            Console.WriteLine(resultado);
            Console.WriteLine("Presiona Enter para continuar.");
            Console.ReadLine();
        }

        public static void MonitorearRecursos(GestionConexion conexion)
        {
            Console.WriteLine("Monitoreando recursos (presiona 'q' para detener)...");
            while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Q)
            {
                try
                {
                    string resultado = conexion.EjecutarComando("free -h && top -b -n 1 | head -n 5");
                    Console.Clear();
                    Console.WriteLine(resultado);
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en monitoreo: {ex.Message}");
                    Thread.Sleep(1000);
                }
            }
        }

        public static void GestionarApache2(GestionConexion conexion)
        {
            Console.WriteLine("Estado de Apache2:");
            string estado = conexion.EjecutarComando("systemctl status apache2");
            Console.WriteLine(estado);

            Console.WriteLine("¿Quieres reiniciar Apache2? (s/n)");
            if (Console.ReadLine().ToLower() == "s")
            {
                string reinicio = conexion.EjecutarComando("sudo systemctl restart apache2");
                Console.WriteLine("Reinicio ejecutado.");
            }

            Console.WriteLine("Comprobando configuración...");
            string config = conexion.EjecutarComando("apache2ctl -t");
            Console.WriteLine(config);
            Console.WriteLine("Presiona Enter para continuar.");
            Console.ReadLine();
        }

        public static void GestionarPostgresql(GestionConexion conexion)
        {
            Console.WriteLine("Estado de PostgreSQL:");
            string estado = conexion.EjecutarComando("systemctl status postgresql");
            Console.WriteLine(estado);

            Console.WriteLine("¿Quieres reiniciar PostgreSQL? (s/n)");
            if (Console.ReadLine().ToLower() == "s")
            {
                string reinicio = conexion.EjecutarComando("sudo systemctl restart postgresql");
                Console.WriteLine("Reinicio ejecutado.");
            }

            Console.WriteLine("Comprobando conexión...");
            string config = conexion.EjecutarComando("sudo -u postgres psql -c \"SELECT 1;\"");
            Console.WriteLine(config);
            Console.WriteLine("Presiona Enter para continuar.");
            Console.ReadLine();
        }
    }
}

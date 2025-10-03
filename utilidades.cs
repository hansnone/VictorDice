using System;

class Utilidades
{
    public static bool PreguntarEsLocal()
    {
        while (true)
        {
            Console.WriteLine("¿El programa corre en el servidor local? (s/n)");
            string respuesta = Console.ReadLine().ToLower();
            if (respuesta == "s") return true;
            if (respuesta == "n") return false;
            Console.WriteLine("Por favor, responde 's' o 'n'.");
        }
    }

    public static string PedirEntrada(string mensaje, bool requerido)
    {
        while (true)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine();
            if (requerido && string.IsNullOrWhiteSpace(entrada))
            {
                Console.WriteLine("Este campo es obligatorio.");
                continue;
            }
            return entrada;
        }
    }
}
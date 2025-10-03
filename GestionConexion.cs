using System;
using Renci.SshNet;
using System.Diagnostics;

public class GestionConexion
{
    private string host;
    private string usuario;
    private string contrasena;
    private bool es_local;

    public GestionConexion(string host, string usuario, string contrasena, bool es_local)
    {
        this.host = host;
        this.usuario = usuario;
        this.contrasena = contrasena;
        this.es_local = es_local;
    }

    public string EjecutarComando(string comando)
    {
        if (es_local)
        {
            // Ejecutar comando localmente
            var proceso = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{comando}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            proceso.Start();
            string salida = proceso.StandardOutput.ReadToEnd();
            string error = proceso.StandardError.ReadToEnd();
            proceso.WaitForExit();
            return string.IsNullOrWhiteSpace(error) ? salida : error;
        }
        else
        {
            // Ejecutar comando por SSH
            using (var cliente = new SshClient(host, usuario, contrasena))
            {
                cliente.Connect();
                var resultado = cliente.RunCommand(comando);
                cliente.Disconnect();
                return resultado.Result;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using Renci.SshNet; // Agrega esta directiva al inicio del archivo


namespace VictorDice
{
    class GestionConexion
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
            try
            {
                if (es_local)
                {
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

                    if (!string.IsNullOrEmpty(error))
                        throw new Exception($"Error en comando: {error}");

                    return salida;
                }
                else
                {
                    using (var cliente = new SshClient(host, usuario, contrasena))
                    {
                        cliente.Connect();
                        var cmd = cliente.CreateCommand(comando);
                        cmd.Execute();
                        string salida = cmd.Result;
                        string error = cmd.Error;

                        if (!string.IsNullOrEmpty(error))
                            throw new Exception($"Error en comando: {error}");

                        cliente.Disconnect();
                        return salida;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ejecutando comando '{comando}': {ex.Message}");
            }
        }
    }
}

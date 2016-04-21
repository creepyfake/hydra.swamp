using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydra.Swamp.Agent.Modules.Repo;

namespace Hydra.Swamp.Agent.Modules.DeployExecutors
{
    public class BatExecutor : IDeployExecutor
    {
        public static string NAME = "BAT";

        private IScriptRepository scriptRepo;

        public BatExecutor(IScriptRepository scriptRepo)
        {
            this.scriptRepo = scriptRepo;
        }

        public DeployResult DeployModule(DeployDescription desc)
        {
            DeployResult result = new DeployResult();
            result.StartTime = DateTime.UtcNow;
            
            // controlla che siano presenti i tool e le impostazioni di ambiente
            checkEnvironment();

            // recupera lo script
            string script = scriptRepo.GetScript(desc.ScriptUrl);
            if (string.IsNullOrEmpty(script))
                throw new ArgumentException(string.Format("Impossibile trovare lo script: {0}", desc.ScriptUrl));
            
            string deployScriptName = string.Format("{0}{1}{2}.{3}.cmd",
                                                    Path.GetDirectoryName(desc.ScriptUrl),
                                                    Path.DirectorySeparatorChar,
                                                    Path.GetFileNameWithoutExtension(desc.ScriptUrl),
                                                    desc.DeployID);
            // sostituisce i parametri nello script
            script =replaceVariables(script);
            // risalva l'istanza specifica di script
            scriptRepo.SaveScript(deployScriptName, script);

            // crea la command line
            string command = deployScriptName;
            desc.Parameters.Values.ToList().ForEach(s => command= command+" "+toCommandArgument(s));

            // lancia lo script
            string error;
            int resultCode = executeCommand(command,out error);
            if (resultCode != 0)
            {
                throw new DeployExecutionException(error);
            }

            result.Success = true;
            result.EndTime = DateTime.UtcNow;
            return result;
        }

        private string toCommandArgument(string parameter)
        {
            if (parameter.Contains(" ") || parameter.Contains("\t"))
                return "\"" + parameter + "\"";
            return parameter;
        }

        private string replaceVariables(string script)
        {
            return script;
        }

        private void checkEnvironment()
        {
            // verifica presenza tool

            // verifica 

            // spara eccezione se manca qualcosa
        }
        private int executeCommand(string command, out string error)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            var process = Process.Start(processInfo);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("output>>" + e.Data);
            process.BeginOutputReadLine();

            StringBuilder errorStr= new StringBuilder();
            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                errorStr.AppendLine(e.Data);
            process.BeginErrorReadLine();
            
            process.WaitForExit();

            int res= process.ExitCode;
            process.Close();

            error = errorStr.ToString();
            return res;
        }

        
    }
}

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
    public class BatExecutor : BaseDeployExecutor
    {
        public static string NAME = "BAT";
        
        public BatExecutor(IScriptRepository scriptRepo, AgentEnvironment env):base(scriptRepo,env)
        {}

        protected override DeployResult doDeploy(DeployDescription desc, DeployResult result)
        {
            // recupera lo script
            string script = scriptRepo.GetScript(desc.ScriptUrl);
            if (string.IsNullOrEmpty(script))
                throw new ArgumentException(string.Format("Impossibile trovare lo script: {0}", desc.ScriptUrl));
            
            string deployScriptName = Path.Combine(workspace,scriptRepo.GetScriptFileName(desc.ScriptUrl));
                
            // sostituisce i parametri nello script
            script =replaceVariables(script,workspace,desc);
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
            
            return result;
        }

        private string toCommandArgument(string parameter)
        {
            if (parameter.Contains(" ") || parameter.Contains("\t"))
                return "\"" + parameter + "\"";
            return parameter;
        }

        private string replaceVariables(string script,string workspace,DeployDescription desc)
        {
            script = script.Replace("#DESTINATION_DIR", moduleDir);
            script = script.Replace("#WORKSPACE", workspace);
            script = script.Replace("#AGENT_DIR", env[AgentParameter.AGENT_DIR.ToString()]);
            script = script.Replace("#BIN_DIR", env[AgentParameter.AGENT_BIN_DIR.ToString()]);
            script = script.Replace("#DATA_DIR", env[AgentParameter.AGENT_DATA_DIR.ToString()]);
            script = script.Replace("#MODULES_DIR", env[AgentParameter.AGENT_MODULES_DIR.ToString()]);
            script = script.Replace("#SCRIPTS_DIR", env[AgentParameter.AGENT_SCRIPTS_DIR.ToString()]);
            script = script.Replace("#MODULE_NAME", desc.ModuleName);
            script = script.Replace("#MODULE_VERSION", desc.ModuleVersion);
            script = script.Replace("#ARTIFACT_URL", desc.ArtifactUrl);
            
            return script;
        }

        protected override void checkEnvironment()
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

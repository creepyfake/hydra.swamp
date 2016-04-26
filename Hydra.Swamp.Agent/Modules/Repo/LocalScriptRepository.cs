using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydra.Swamp.Agent.Modules.DeployExecutors;
using Newtonsoft.Json;

namespace Hydra.Swamp.Agent.Modules.Repo
{
    public class LocalScriptRepository : IScriptRepository
    {
        private AgentEnvironment env;

        public LocalScriptRepository(AgentEnvironment env)
        {
            this.env = env;
        }

        public string GetScript(string scriptUrl)
        {
            string repoUrl = env[AgentParameter.AGENT_DIR.ToString()];
            string scriptFile = Path.Combine(repoUrl,"localrepo", scriptUrl);
            bool exists = File.Exists(scriptFile);
            if (!exists)
                return null;
            string text = File.ReadAllText(scriptFile);
            return text;
        }

        public void SaveScript(string deployScriptName, string scriptContent)
        {
            File.WriteAllText(deployScriptName, scriptContent);
        }

        public string CreateDeployWorkspace(string deployId)
        {
            string workspace = Path.Combine(env[AgentParameter.AGENT_DATA_DIR.ToString()], deployId);
            if (!Directory.Exists(workspace))
                Directory.CreateDirectory(workspace);
            return workspace;
        }

        public string GetScriptFileName(string scriptUrl)
        {
            return Path.GetFileName(scriptUrl);
        }

        public void DeleteDeployWorkspace(string workspace)
        {
            if (Directory.Exists(workspace))
                Directory.Delete(workspace,true);
        }

        public void SaveDescFile(string moduleDir, ManagedModule mod)
        {
            if(!Directory.Exists(moduleDir))
                throw new DeployExecutionException("Non esiste la directory del modulo "+moduleDir);
            string filename = Path.Combine(moduleDir, "module.desc");
            File.WriteAllText(filename,JsonConvert.SerializeObject(mod));
        }
    }
}

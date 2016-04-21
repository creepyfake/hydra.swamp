using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydra.Sys.Config;
using Newtonsoft.Json;

namespace Hydra.Swamp.Agent
{
    public class AgentEnvironment
    {
        private Dictionary<string, string> parameters=new Dictionary<string, string>();

        public string this[string key]
        {
            get { return parameters.ContainsKey(key) ? parameters[key] : null; }
        }

        public AgentEnvironment()
        {
            fillParameters();
        }

        private void fillParameters()
        {
            //
            AgentConfiguration cfgEnv = HEnvironment.GetConfig<AgentConfiguration>("ANY");

            AgentConfiguration cfgLocal = loadLocalCfg();

            // Working dir
            parameters.Add(AgentParameter.AGENT_DIR.ToString(),
                Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "..");
            if (cfgEnv!=null && !string.IsNullOrEmpty(cfgEnv.WorkingDir))
                parameters[AgentParameter.AGENT_DIR.ToString()] = cfgEnv.WorkingDir;
            if(cfgLocal!=null && !string.IsNullOrEmpty(cfgLocal.WorkingDir))
                parameters[AgentParameter.AGENT_DIR.ToString()] = cfgLocal.WorkingDir;

            //bin dir
            parameters.Add(AgentParameter.AGENT_BIN_DIR.ToString(),
                Path.Combine(Directory.GetCurrentDirectory(),"..","bin"));
            if (cfgEnv != null && !string.IsNullOrEmpty(cfgEnv.BinDir))
                parameters[AgentParameter.AGENT_BIN_DIR.ToString()] = cfgEnv.BinDir;
            if (cfgLocal != null && !string.IsNullOrEmpty(cfgLocal.BinDir))
                parameters[AgentParameter.AGENT_BIN_DIR.ToString()] = cfgLocal.BinDir;

            //data
            parameters.Add(AgentParameter.AGENT_DATA_DIR.ToString(),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "data"));
            if (cfgEnv != null && !string.IsNullOrEmpty(cfgEnv.DataDir))
                parameters[AgentParameter.AGENT_DATA_DIR.ToString()] = cfgEnv.DataDir;
            if (cfgLocal != null && !string.IsNullOrEmpty(cfgLocal.DataDir))
                parameters[AgentParameter.AGENT_DATA_DIR.ToString()] = cfgLocal.DataDir;

            //modules
            parameters.Add(AgentParameter.AGENT_MODULES_DIR.ToString(),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "modules"));
            if (cfgEnv != null && !string.IsNullOrEmpty(cfgEnv.ModulesDir))
                parameters[AgentParameter.AGENT_MODULES_DIR.ToString()] = cfgEnv.ModulesDir;
            if (cfgLocal != null && !string.IsNullOrEmpty(cfgLocal.ModulesDir))
                parameters[AgentParameter.AGENT_MODULES_DIR.ToString()] = cfgLocal.ModulesDir;


            //scripts
            parameters.Add(AgentParameter.AGENT_SCRIPTS_DIR.ToString(),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "scripts"));
            if (cfgEnv != null && !string.IsNullOrEmpty(cfgEnv.ScriptsDir))
                parameters[AgentParameter.AGENT_SCRIPTS_DIR.ToString()] = cfgEnv.ScriptsDir;
            if (cfgLocal != null && !string.IsNullOrEmpty(cfgLocal.ScriptsDir))
                parameters[AgentParameter.AGENT_SCRIPTS_DIR.ToString()] = cfgLocal.ScriptsDir;


        }

        private AgentConfiguration loadLocalCfg()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "conf", "agent.cfg");
            try
            {
                if (File.Exists(filePath))
                {
                    string file = File.ReadAllText(filePath);
                    AgentConfiguration ret = JsonConvert.DeserializeObject<AgentConfiguration>(file);
                    return ret;
                }
            }
            catch (Exception)
            {
                //non importa
            }
            return null;
        }
    }

    public enum AgentParameter
    {
        AGENT_DIR,AGENT_BIN_DIR,AGENT_MODULES_DIR,AGENT_DATA_DIR,AGENT_SCRIPTS_DIR
    }
}

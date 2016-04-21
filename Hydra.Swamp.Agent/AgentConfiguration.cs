using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydra.Sys.Config;

namespace Hydra.Swamp.Agent
{
    public class AgentConfiguration : BaseConfiguration
    {
        //AGENT_DIR,AGENT_BIN_DIR,AGENT_MODULES_DIR,AGENT_DATA_DIR,AGENT_SCRIPTS_DIR
        public string WorkingDir { get; set; }
        public string BinDir { get; set; }
        public string ModulesDir { get; set; }
        public string DataDir { get; set; }
        public string ScriptsDir { get; set; }


    }
}

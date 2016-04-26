using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydra.Swamp.Agent.Modules;
using Hydra.Swamp.Agent.Modules.Repo;
using Hydra.Swamp.Agent.Status;
using Hydra.Sys.IOC;

namespace Hydra.Swamp.Agent
{
    public class AgentDependencies : IDependenciesMap
    {
        public void RegisterDependencies(IHydraContainer cont)
        {
            AgentEnvironment env = new AgentEnvironment();
            // TRANSIENT
            cont.ForTypeUse<IScriptRepository,LocalScriptRepository>();
            // SINGLETONS
            cont.ForTypeUseSingleton<ModuleManager>(new ModuleManager(cont,env));
            cont.ForTypeUseSingleton<StatusManager>(new StatusManager());

        }
    }
}

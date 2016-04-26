using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydra.Swamp.Agent.Modules.Repo;

namespace Hydra.Swamp.Agent.Modules.DeployExecutors
{
    public class NantExecutor : BaseDeployExecutor
    {
        public static string NAME = "NANT";
        

        protected override DeployResult doDeploy(DeployDescription desc, DeployResult result)
        {
            throw new NotImplementedException();
        }

        protected override void checkEnvironment()
        {
            throw new NotImplementedException();
        }

        public NantExecutor(IScriptRepository scriptRepo,AgentEnvironment env) : base(scriptRepo,env)
        {
        }
    }
}

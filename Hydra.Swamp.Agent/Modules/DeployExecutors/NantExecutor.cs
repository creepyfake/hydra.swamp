using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Swamp.Agent.Modules.DeployExecutors
{
    public class NantExecutor : IDeployExecutor
    {
        public static string NAME = "NANT";

        public DeployResult DeployModule(DeployDescription desc)
        {
            throw new NotImplementedException();
        }
    }
}

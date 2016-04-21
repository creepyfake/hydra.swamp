using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Swamp.Agent.Modules.DeployExecutors
{
    public class DeployExecutionException : Exception
    {
        public DeployExecutionException(string message): base(message)
        {}
    }
}

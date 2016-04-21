using System.Threading;
using System.Threading.Tasks;

namespace Hydra.Swamp.Agent.Modules
{
    public class DeployTask
    {
        public DeployDescription Description { get; set; }
        public Task<DeployResult> Task { get; set; }
        public CancellationTokenSource Token { get; set; }
        public DeployResult Result { get; set; }
    }
}
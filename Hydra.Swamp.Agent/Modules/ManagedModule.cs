using System;

namespace Hydra.Swamp.Agent.Modules
{
    public class ManagedModule
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public DateTime DeployTime { get; set; }
        public int Instance { get; set; }
    }
}
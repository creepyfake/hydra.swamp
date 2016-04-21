using System;

namespace Hydra.Swamp.Agent.Modules
{
    public class DeployResult
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
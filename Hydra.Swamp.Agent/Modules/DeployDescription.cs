
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hydra.Swamp.Agent.Modules
{
    public class DeployDescription
    {
        public DeployDescription()
        {
            DeployID = Guid.NewGuid().ToString();
            Parameters = new Dictionary<string, string>();
        }
        public string DeployID { get; private set; }
        public string DeployType { get; set; }
        public string ModuleName { get; set; }
        public string ModuleVersion { get; set; }
        public int RequestedInstance { get; set; }
        public string ScriptUrl { get; set; }
        public string ArtifactUrl { get; set; }
        public Dictionary<string,string> Parameters { get; private set; }
        
    }
}
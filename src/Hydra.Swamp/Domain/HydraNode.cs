using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hydra.Swamp.Domain.Agents;

namespace Hydra.Swamp.Domain
{
    public class HydraNode
    {
        public string Name { get; set; }
        public string OS { get; set; }
        public int DiskSize { get; set; }
        public int MemSize { get; set; }
        public string Address { get; set; }
        public List<string> Capabilities { get; set; }
        public string Description { get; set; }
        
        List<HydraComponent> components = new List<HydraComponent>();
        public List<HydraComponent> Components { get; set; }

        SwampAgent agent;
    }
}

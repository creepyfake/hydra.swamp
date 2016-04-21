using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Swamp.Agent.Status
{
    public class MachineStatus
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int CPU { get; set; }
        public int RAM { get; set; }
        public int DISK { get; set; }
    }
}

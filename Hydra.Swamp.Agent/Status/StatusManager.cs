using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Swamp.Agent.Status
{
    public class StatusManager
    {
        private MachineStatus stat;

        public void RefreshInfo()
        {
            // read machine name & address

            // get disk usage

            // get cpu

            // get RAM
        }

        public MachineStatus GetMachineStatus()
        {
            return fillMachineStatus();
        }

        private MachineStatus fillMachineStatus()
        {
            return stat;
        }
    }
}

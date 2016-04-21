using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Hydra.Swamp.Agent.Status;
using Newtonsoft.Json;

namespace Hydra.Swamp.Agent.Controller
{
    [RoutePrefix("status")]
    public class StatusController : ApiController
    {
        private StatusManager mng;

        public StatusController(StatusManager mng)
        {
            this.mng = mng;
        }

        [Route("agent_status")]
        [HttpGet]
        public string GetAgentStatus()
        {

            return JsonConvert.SerializeObject("");
        }

        [Route("machine_status")]
        [HttpGet]
        public string GetMachineStatus()
        {
            return JsonConvert.SerializeObject(mng.GetMachineStatus());
        }


        
    }
}

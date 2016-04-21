using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;

namespace Hydra.Swamp.Agent.Controller
{
    [RoutePrefix("deploy")]
    public class DeployController : ApiController
    {

        [Route("new")]
        [HttpPost]
        public string CreateNewDeploy()
        {

            return JsonConvert.SerializeObject("");
        }

        


    }
}

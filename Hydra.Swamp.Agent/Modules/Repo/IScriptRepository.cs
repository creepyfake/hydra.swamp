using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Swamp.Agent.Modules.Repo
{
    public interface IScriptRepository
    {
        string GetScript(string scriptUrl);
        void SaveScript(string deployScriptName, string script);
    }
}

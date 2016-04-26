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
        void SaveScript(string deployScriptName, string scriptContent);
        string CreateDeployWorkspace(string deployId);
        string GetScriptFileName(string scriptUrl);
        void DeleteDeployWorkspace(string workspace);
        void SaveDescFile(string moduleDir, ManagedModule mod);
    }
}

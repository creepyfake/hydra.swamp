using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Swamp.Agent.Modules.Repo
{
    public class LocalScriptRepository : IScriptRepository
    {
        public string GetScript(string scriptUrl)
        {
            bool exists = File.Exists(scriptUrl);
            if (!exists)
                return null;
            string text = File.ReadAllText(scriptUrl);
            return text;
        }

        public void SaveScript(string deployScriptName, string script)
        {
            File.WriteAllText(deployScriptName,script);
        }
    }
}

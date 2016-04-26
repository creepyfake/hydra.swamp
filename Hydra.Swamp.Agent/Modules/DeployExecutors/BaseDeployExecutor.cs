using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydra.Swamp.Agent.Modules.Repo;

namespace Hydra.Swamp.Agent.Modules.DeployExecutors
{
    public abstract class BaseDeployExecutor
    {
        protected string workspace;
        protected string moduleDir;
        protected IScriptRepository scriptRepo;
        protected AgentEnvironment env;
        protected BaseDeployExecutor(IScriptRepository scriptRepo, AgentEnvironment env)
        {
            this.scriptRepo = scriptRepo;
            this.env = env;
        }
        /// <summary>
        /// Esegue il lavoro di deploy (script)
        /// </summary>
        /// <param name="desc">Descrizione del job di deploy</param>
        /// <returns>Risultato del deploy</returns>
        public DeployResult DeployModule(DeployDescription desc)
        {
            DeployResult result = new DeployResult();
            result.StartTime = DateTime.UtcNow;

            // controlla che siano presenti i tool e le impostazioni di ambiente
            checkEnvironment();

            // crea lo spazio di lavoro per il deploy
            workspace = scriptRepo.CreateDeployWorkspace(desc.DeployID);
            // crea il nome della directory di destinazione del modulo
            moduleDir = string.Format("{0}_{1}_{2}", desc.RequestedInstance, desc.ModuleName, desc.ModuleVersion);
            moduleDir = Path.Combine(env[AgentParameter.AGENT_MODULES_DIR.ToString()], moduleDir);
            // lancia il lavoro di deploy
            result = doDeploy(desc, result);

            // elimina il workspace
            scriptRepo.DeleteDeployWorkspace(workspace);

            // se successo, crea il file descrittore
            if (result.Success)
            {
                ManagedModule mod = new ManagedModule
                {
                    DeployTime = DateTime.UtcNow,
                    Name = desc.ModuleName,
                    Version = desc.ModuleVersion,
                    Instance = desc.RequestedInstance
                };
                //salva il file descrittore nella directory del modulo
                scriptRepo.SaveDescFile(moduleDir, mod);
            }

            result.EndTime = DateTime.UtcNow;
            return result;
        }
        /// <summary>
        /// Esegue il lavoro di deploy (script)
        /// </summary>
        /// <param name="desc">Descrizione del job di deploy</param>
        /// <param name="result">Risultato del deploy</param>
        /// <returns></returns>
        protected abstract DeployResult doDeploy(DeployDescription desc,DeployResult result);
        /// <summary>
        /// Controlla che tutti i tool e le variabili di ambiente necessarie siano disponibili
        /// </summary>
        protected abstract void checkEnvironment();
    }
}

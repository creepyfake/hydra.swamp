using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hydra.Swamp.Agent.Modules.DeployExecutors;
using Hydra.Swamp.Agent.Modules.Repo;
using Hydra.Sys.IOC;

namespace Hydra.Swamp.Agent.Modules
{
    public class ModuleManager
    {
        readonly List<ManagedModule> modules;

        public ManagedModule[] Modules {get { return modules.ToArray(); } }
        
        private Dictionary<string, DeployTask> deploymentTasks;

        private IHydraContainer cont;

        public ModuleManager(IHydraContainer container)
        {
            this.cont = container;
            deploymentTasks = new Dictionary<string, DeployTask>();
            modules = new List<ManagedModule>();
        }

        /// <summary>
        /// Richiede il deploy di un modulo
        /// </summary>
        /// <param name="desc">descriptor del deploy</param>
        /// <returns></returns>
        public async Task DeployModule(DeployDescription desc)
        {
            IDeployExecutor executor=getExecutor(desc);
            
            //lancia il task di deploy
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            Task<DeployResult> task = new Task<DeployResult>(
                () => executor.DeployModule(desc),
                token
                );
            DeployTask dTask = new DeployTask
            {
                Description = desc,
                Task = task,
                Token = tokenSource
            };
            deploymentTasks.Add(desc.DeployID,dTask);

            task.Start();
            await task;

            DeployResult r = task.Result;
            r.Success = !task.IsFaulted;
            if (!r.Success && task.Exception!=null)
                r.Error = task.Exception.Message;
            
            dTask.Result = r;
            
            //se è un successo chiedi un refresh della lista dei moduli
            if(r.Success)
                refreshModuleList();
        }
        
        // in base al tipo di deploy fa partire un determinato task
        private IDeployExecutor getExecutor(DeployDescription desc)
        {
            if (desc.DeployType == BatExecutor.NAME)
                return new BatExecutor(cont.GetInstance<IScriptRepository>());
            if (desc.DeployType == NantExecutor.NAME)
                return new NantExecutor();

            return null;
        }

        private void refreshModuleList()
        {
            // legge le informazioni dei moduli installati
            //init
            modules.Clear();
            
            Directory.EnumerateDirectories()

        }
        /// <summary>
        /// Richiede di cancellare un deploy in corso
        /// </summary>
        /// <param name="desc"></param>
        public void CancelDeployment(DeployDescription desc)
        {
            if (!deploymentTasks.ContainsKey(desc.DeployID))
                return;
            DeployTask task = deploymentTasks[desc.DeployID];
            if(!task.Token.IsCancellationRequested && task.Token.Token.CanBeCanceled)
                task.Token.Cancel();
        }
        /// <summary>
        /// Ottiene la lista di tutti i deploy fatti o in corso di opera
        /// </summary>
        /// <returns></returns>
        public List<DeployTask> GetDeployments()
        {
            return deploymentTasks.Values.ToList();
        }

    }
}

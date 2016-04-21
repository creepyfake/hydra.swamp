using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydra.Swamp.Agent.Modules;
using Hydra.Swamp.Agent.Modules.DeployExecutors;
using Hydra.Swamp.Agent.Modules.Repo;
using Hydra.Sys.IOC.SimpleInjector;
using NUnit.Framework;

namespace Hydra.Swamp.Agent.Tests
{
    [TestFixture]
    public class ModuleManagerTests
    {
        [Test]
        public async void TestBatTask()
        {
            
            SimpleInjectorContainer cont = new SimpleInjectorContainer();
            cont.ForTypeUse<IScriptRepository,LocalScriptRepository>();

            ModuleManager manager = new ModuleManager(cont);

            string localDir = Environment.CurrentDirectory + @"\..\scripts\";
            DeployDescription desc = new DeployDescription
            {
                DeployType = BatExecutor.NAME,
                ModuleName = "test",
                ArtifactUrl = "",
                ScriptUrl = localDir + "script_test.cmd"
            };
            desc.Parameters.Add("arg1", "pippo");
            desc.Parameters.Add("arg2", "foo bar");

            Task t =manager.DeployModule(desc);
            await t;

            Assert.That(manager.GetDeployments().Exists(dt => dt.Description.DeployID == desc.DeployID));
            Assert.That(manager.GetDeployments().Single(dt=>dt.Description.DeployID==desc.DeployID).Result.Success);
        }

    }
}

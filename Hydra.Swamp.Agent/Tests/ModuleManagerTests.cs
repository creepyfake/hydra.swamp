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
        public void TestGetNextInstance()
        {
            SimpleInjectorContainer cont = new SimpleInjectorContainer();
            cont.ForTypeUse<IScriptRepository, LocalScriptRepository>();
            AgentEnvironment env = new AgentEnvironment();

            ModuleManager manager = new ModuleManager(cont, env);

            Assert.That(manager.GetNextInstance(new List<ManagedModule>(), "testo", "1.0.0"), Is.EqualTo(0));

            List<ManagedModule> modules = new List<ManagedModule>();
            modules.Add(new ManagedModule
            {
                Name = "test",
                Version = "1.0.0",
                Instance = 0
            });
        
            Assert.That(manager.GetNextInstance(modules,"testAA","1.0.0"),Is.EqualTo(0));

            Assert.That(manager.GetNextInstance(modules, "test", "1.0.0"), Is.EqualTo(1));

            modules.Add(new ManagedModule
            {
                Name = "test",
                Version = "1.0.0",
                Instance = 2
            });
            Assert.That(manager.GetNextInstance(modules, "test", "1.0.0"), Is.EqualTo(3));
        }

        [Test]
        public async void TestBatTask()
        {
            
            SimpleInjectorContainer cont = new SimpleInjectorContainer();
            cont.ForTypeUse<IScriptRepository,LocalScriptRepository>();
            AgentEnvironment env = new AgentEnvironment();

            ModuleManager manager = new ModuleManager(cont,env);
            
            DeployDescription desc = new DeployDescription
            {
                DeployType = BatExecutor.NAME,
                ModuleName = "test",
                ModuleVersion = "v1.0.0.0",
                ArtifactUrl = "test_v1.0.0.0.zip",
                ScriptUrl = "script_test.cmd"
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

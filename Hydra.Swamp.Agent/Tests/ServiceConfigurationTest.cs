using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydra.Sys.Config;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Hydra.Swamp.Agent.Tests
{
    [TestFixture]
    public class ServiceConfigurationTest
    {
        [Test]
        public void TestConfigurationLoading()
        {
            AgentConfiguration cfg = HEnvironment.GetConfig<AgentConfiguration>("ANY");
            Assert.That(cfg,Is.Not.Null);
            Assert.That(cfg.BinDir,Is.EqualTo("bin"));
            
            AgentEnvironment env = new AgentEnvironment();

            string binPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "bin");
            Assert.That(env[AgentParameter.AGENT_BIN_DIR.ToString()],Is.EqualTo(binPath));

            Assert.That(cfg.ModulesDir,Is.EqualTo(""));
            Assert.That(env[AgentParameter.AGENT_MODULES_DIR.ToString()],Is.EqualTo(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "modules")));
        }

        [Test]
        public void TestLocalConfigurationLoading()
        {
            AgentConfiguration cfg = HEnvironment.GetConfig<AgentConfiguration>("ANY");
            Assert.That(cfg, Is.Not.Null);
            Assert.That(cfg.BinDir, Is.EqualTo("bin"));

            //scrivo un file di configurazione temporaneo
            string installDir = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;

            AgentConfiguration acfg = new AgentConfiguration {ModulesDir = Path.Combine(installDir,"my_modules")};
            Directory.CreateDirectory(Path.Combine(installDir, "conf"));
            File.WriteAllText(Path.Combine(installDir,"conf", "agent.cfg"),JsonConvert.SerializeObject(acfg));

            AgentEnvironment env = new AgentEnvironment();

            string binPath = Path.Combine(installDir, "bin");
            Assert.That(env[AgentParameter.AGENT_BIN_DIR.ToString()], Is.EqualTo(binPath));

            Assert.That(cfg.ModulesDir, Is.EqualTo(""));
            Assert.That(env[AgentParameter.AGENT_MODULES_DIR.ToString()], Is.EqualTo(Path.Combine(installDir, "my_modules")));

            //pulisco file
            File.Delete(Path.Combine(installDir, "conf", "agent.cfg"));
            Directory.Delete(Path.Combine(installDir, "conf"));
        }


    }
}

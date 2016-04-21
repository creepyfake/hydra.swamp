using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydra.Swamp.Agent.Modules;
using Hydra.Swamp.Agent.Modules.DeployExecutors;
using Hydra.Swamp.Agent.Modules.Repo;
using NUnit.Framework;

namespace Hydra.Swamp.Agent.Tests
{
    [TestFixture]
    public class BatExecutorTests
    {
        [Test]
        public void TestScriptExec()
        {
            LocalScriptRepository repo = new LocalScriptRepository();
            BatExecutor executor = new BatExecutor(repo);

            DeployDescription desc = new DeployDescription
            {
                DeployType = BatExecutor.NAME,
                ModuleName = "test",
                ArtifactUrl = "",
                ScriptUrl = "script_test.cmd"
            };
            desc.Parameters.Add("arg1","pippo");
            desc.Parameters.Add("arg2", "foo bar");

            //DeployResult res =executor.DeployModule(desc);
            Assert.Throws<ArgumentException>(() => executor.DeployModule(desc));

            string localDir = Environment.CurrentDirectory+@"\..\scripts\";
            desc = new DeployDescription
            {
                DeployType = BatExecutor.NAME,
                ModuleName = "test",
                ArtifactUrl = "",
                ScriptUrl = localDir+"script_test.cmd"
            };
            desc.Parameters.Add("arg1", "pippo");
            desc.Parameters.Add("arg2", "foo bar");
            desc.Parameters.Add("crash", "true");

            //test forced crash
            Assert.Throws<DeployExecutionException>(() => executor.DeployModule(desc));

            desc = new DeployDescription
            {
                DeployType = BatExecutor.NAME,
                ModuleName = "test",
                ArtifactUrl = "",
                ScriptUrl = localDir + "script_test.cmd"
            };
            desc.Parameters.Add("arg1", "pippo");
            desc.Parameters.Add("arg2", "foo bar");

            DeployResult res =executor.DeployModule(desc);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Success, Is.True);
            Assert.That(res.Error, Is.Null);
            Console.WriteLine("Deploy runtime: "+(res.EndTime-res.StartTime));
        }
    }
}

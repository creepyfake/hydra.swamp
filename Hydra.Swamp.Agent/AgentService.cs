using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydra.Sys.Config;
using Hydra.Sys.HComp;
using Hydra.Sys.HComp.Config;
using Hydra.Sys.IOC;
using Hydra.Sys.IOC.SimpleInjector;
using Hydra.Sys.Logging;
using Hydra.Sys.Logging.ELK;
using Hydra.Sys.WebAPI;

namespace Hydra.Swamp.Agent
{
    public class AgentService : HydraComponent
    {
        protected override IHydraWebApiConfigurator GetEndpointWebApiConfigurator()
        {
            return null;
        }

        protected override IAppenderFactory GetAppenderFactory()
        {
            ComponentConfiguration cfg = HEnvironment.GetConfig<ComponentConfiguration>("ANY");
            if (cfg == null)
            {
                throw new HydraConfigException("MISSING configuration: HEnvironment.GetConfig<ComponentConfiguration>(\"ANY\") : no data");
            }
            ELKAppenderFactory fact = new ELKAppenderFactory(ComponentID, HEnvironment.LocalSetup.ApplicationName, cfg.ELKLogging.ESUrl, cfg.ELKLogging.ESPort);
            return fact;
        }

        protected override string GetEndpointServiceName()
        {
            return "HydraSwampAgent";
        }

        protected override string GetEndpointDisplayName()
        {
            return "HydraSwampAgent";
        }

        protected override string GetEndpointDescription()
        {
            return "Hydra Swamp Agent - managing deploy of modules on local machine";
        }

        protected override IHydraContainer getNewIOCContainer()
        {
            return new SimpleInjectorContainer();
        }

        protected override void PreContainerConfiguration(IHydraContainer container)
        {
            return;
        }

        protected override void PostContainerConfiguration(IHydraContainer container)
        {
            return;
        }

        protected override bool OnStart()
        {
            
            return true;
        }

        protected override bool OnStop()
        {
            return true;
        }


        public static void Main(string[] args)
        {
            AgentService srv = new AgentService();
        }
    }
}

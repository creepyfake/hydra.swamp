namespace Hydra.Swamp.Domain.Agents
{
    /// <summary>
    /// Agent installato sulla macchina dove vengono messe in run le istanza dei componenti
    /// </summary>
    public class SwampAgent
    {
        public string AgentName { get; set; }

        public string AgentEndpoint { get; set; }

        public bool Reachable { get {return testCommunication();} private set {} }

        #region Agent Interface

        private bool testCommunication()
        {
            //verifica che l'agent risponda
            return true;
        }

        public AgentRequestResult DeployComponent(ComponentDescription desc)
        {
            return null;
        }

        public AgentRequestResult RemoveComponent(ComponentDescription desc)
        {
            return null;
        }

        #endregion

    }
}

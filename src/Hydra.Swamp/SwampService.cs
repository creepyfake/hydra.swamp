using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hydra.Swamp.Domain;

namespace Hydra.Swamp
{
    public class SwampService
    {
        /// <summary>
        /// Recupera l'oggetto nodo
        /// </summary>
        /// <param name="name">nome del nodo</param>
        /// <returns></returns>
        public HydraNode GetNode(string name)
        {
            return null;
        }
        /// <summary>
        /// Recupera la lista dei componenti su uno specifico nodo
        /// </summary>
        /// <param name="nodeName">Il nome del nodo</param>
        /// <returns></returns>
        public List<HydraComponent> GetNodeComponents(string nodeName)
        {
            return null;
        }
        /// <summary>
        /// Recupera la lista dei nodi che hanno un'istanza del componente specificato
        /// </summary>
        /// <param name="compName">Nome del componente</param>
        /// <param name="compVersion">Stringa *.*.* dove * fa match su qualsiasi stringa</param>
        /// <returns></returns>
        public List<HydraNode> GetAllNodesWithComponent(string compName, string compVersion)
        {
            return null;
        }
    }
}

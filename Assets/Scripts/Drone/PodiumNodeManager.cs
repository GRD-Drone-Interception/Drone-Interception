using System.Collections.Generic;
using UnityEngine;

namespace Drone
{
    public class PodiumNodeManager : MonoBehaviour
    {
        public List<PodiumNode> PodiumNodes => podiumNodes;
        [SerializeField] private List<PodiumNode> podiumNodes;

        private void OnDrawGizmos()
        {
            if (podiumNodes.Count <= 1) { return;}
           
            Gizmos.color = Color.red;
            for (int i = 0; i < podiumNodes.Count-1; i++)
            {
                Gizmos.DrawLine(podiumNodes[i].transform.position, podiumNodes[i+1].transform.position);
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class NodeManager : MonoBehaviour
    {
        public List<Node> PodiumNodes => _podiumNodes;
        private readonly List<Node> _podiumNodes = new();

        private void Awake() => _podiumNodes.AddRange(GetComponentsInChildren<Node>());

        private void OnDrawGizmos()
        {
            if (_podiumNodes.Count <= 1) { return;}
           
            Gizmos.color = Color.red;
            for (int i = 0; i < _podiumNodes.Count-1; i++)
            {
                Gizmos.DrawLine(_podiumNodes[i].transform.position, _podiumNodes[i+1].transform.position);
            }
        }
    }
}
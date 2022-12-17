using System;
using System.Collections.Generic;
using UnityEngine;

namespace Drone
{
    public class PodiumNodeManager : MonoBehaviour
    {
        public List<PodiumNode> PodiumNodes => _podiumNodes;
        private readonly List<PodiumNode> _podiumNodes = new();

        private void Awake() => _podiumNodes.AddRange(GetComponentsInChildren<PodiumNode>());

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
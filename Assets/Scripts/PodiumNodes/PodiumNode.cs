using UnityEngine;

namespace PodiumNodes
{
    public class PodiumNode : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1.0f,0.5f,0f);
            Gizmos.DrawSphere(transform.position, 0.2f);
        }
    }
}
using UnityEngine;

namespace Utility
{
    public class Node : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1.0f,0.5f,0f);
            Gizmos.DrawSphere(transform.position, 0.2f);
        }
    }
}
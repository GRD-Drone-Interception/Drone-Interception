using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        transform.position = new Vector3(target.position.x, target.position.y + 175, target.position.z);
    }
}

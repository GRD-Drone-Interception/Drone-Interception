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

        transform.position = new Vector3(target.position.x, target.position.y + 200f, target.position.z);
    }
}

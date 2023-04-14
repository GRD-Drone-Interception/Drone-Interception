using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{


    public float moveSpeed = 10f;




    void Update()
    {
        float xInput = -1 * Input.GetAxis("Horizontal");
        float zInput = -1 * Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(xInput, 0f, zInput);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}

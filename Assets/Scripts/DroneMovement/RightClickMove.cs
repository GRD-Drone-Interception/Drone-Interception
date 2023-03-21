using UnityEngine;

public class RightClickMove : MonoBehaviour
{
    public GameObject target; // the object to move

    private void Update()
    {
        // Detect right-click
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("click!");
            // Raycast to detect ground at clicked position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Move target to clicked position
                target.transform.position = hit.point;
            }
        }
    }
}

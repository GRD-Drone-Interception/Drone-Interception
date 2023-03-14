using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightClickMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Move the object instantly to the clicked point
                transform.position = hit.point;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}



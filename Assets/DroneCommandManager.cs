using DroneBehaviours.Scripts;
using UnityEngine;

public class DroneCommandManager : MonoBehaviour
{
    [SerializeField] private Camera tacticalCamera;
    [SerializeField] private GameObject waypointMarkerPrefab;

    private void Update()
    {
        //Debug.Log($"Count: {DroneManager.Instance.SelectedDrones.Count}");
        
        if (DroneManager.Instance.SelectedDrones.Count <= 0)
        {
            return;
        }
        
        Ray ray = tacticalCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (FindObjectOfType<WaypointBobbing>() != null)
                {
                    Destroy(FindObjectOfType<WaypointBobbing>().transform.parent.gameObject);
                }
                
                DroneManager.Instance.Move(hit.point + Vector3.up*2);
                var marker = Instantiate(waypointMarkerPrefab);
                marker.transform.position = hit.point;
            }
        }
    }
}

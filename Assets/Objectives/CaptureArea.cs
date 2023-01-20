using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureArea : MonoBehaviour
{
    private List<CaptureZoneCollider> captureZoneList = new List<CaptureZoneCollider>();

    private float progress;
    private float captureRate = 1f;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            CaptureZoneCollider captureZone = child.GetComponent<CaptureZoneCollider>();
            if(captureZone != null)
            {
                captureZoneList.Add(captureZone);
            }
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        int playersInArea = 0;
        foreach (CaptureZoneCollider zone in captureZoneList)
        {
            playersInArea += zone.GetUnitsInArea().Count;
        }

        progress += playersInArea * captureRate * Time.deltaTime;

        Debug.Log($"players in area: {playersInArea}; progress: {progress}");
    }
}

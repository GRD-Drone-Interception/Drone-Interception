using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureArea : MonoBehaviour
{
    private List<CaptureZoneCollider> captureZoneList = new List<CaptureZoneCollider>();

    private float progress;
    private float captureRate = 0.1f;

    [SerializeField] private ObjetiveCaptureState objectiveState;

    [SerializeField] private GameObject captureZoneColliderContainer;

    private void Awake()
    {
        foreach (Transform child in captureZoneColliderContainer.transform)
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
        switch (objectiveState)
        {
            case ObjetiveCaptureState.Neutral:
                List<DroneUnit> unitsInArea = new List<DroneUnit>();

                foreach (CaptureZoneCollider zone in captureZoneList)
                {
                    foreach (DroneUnit unit in zone.GetUnitsInArea())
                    {
                        if (!unitsInArea.Contains(unit))
                        {
                            unitsInArea.Add(unit);
                        }
                    }
                }

                progress += unitsInArea.Count * captureRate * Time.deltaTime;

                if(progress >= 1f)
                {
                    objectiveState = ObjetiveCaptureState.Captured;
                    Debug.Log("Objective Captured");
                }

                Debug.Log($"players in area: {unitsInArea.Count}; progress: {progress}");
                break;

            case ObjetiveCaptureState.Capturing:
                break;

            case ObjetiveCaptureState.Contested:
                break;

            case ObjetiveCaptureState.Captured:
                break;

            default:
                break;
        }
    }
}

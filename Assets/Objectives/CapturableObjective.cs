using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturableObjective : MonoBehaviour, ICapturable
{
    [SerializeField] private ObjetiveCaptureState captureState;
    public ObjetiveCaptureState CaptureState => captureState;

    private int defendersOnPoint;
    public int DefendersOnPoint => defendersOnPoint;

    private int attackersOnPoint;
    public int AttackersOnPoint => attackersOnPoint;


    [SerializeField] private PlayerTeam controllingTeam;
    public PlayerTeam ControllingTeam => controllingTeam;

    private List<CaptureZoneCollider> captureZoneList = new List<CaptureZoneCollider>();

    private float progress;
    private float captureRate = 0.1f;

    [SerializeField] private GameObject captureArea;

    private void Awake()
    {
        foreach (Transform child in captureArea.transform)
        {
            CaptureZoneCollider captureZone = child.GetComponent<CaptureZoneCollider>();
            if (captureZone != null)
            {
                captureZoneList.Add(captureZone);
            }
        }
    }

    void Start()
    {
        if(controllingTeam == PlayerTeam.Offensive)
        {
            progress = -1;
        }
        else if (controllingTeam == PlayerTeam.Defensive)
        {
            progress = 1;
        }
    }

    public void Capture(PlayerTeam capturingTeam)
    {
        captureState = ObjetiveCaptureState.Captured;
        controllingTeam = capturingTeam;
    }

    void Update()
    {
        /*switch (captureState)
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

                if (progress <= 0f || progress >= 1f)
                {
                    captureState = ObjetiveCaptureState.Captured;
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
        }*/

        List<DroneUnit> team1InArea = new List<DroneUnit>();
        List<DroneUnit> team2InArea = new List<DroneUnit>();

        foreach (CaptureZoneCollider zone in captureZoneList)
        {
            foreach (DroneUnit unit in zone.GetTeam1UnitsInArea())
            {
                if (!team1InArea.Contains(unit))
                {
                    team1InArea.Add(unit);
                }
            }
            foreach (DroneUnit unit in zone.GetTeam2UnitsInArea())
            {
                if (!team2InArea.Contains(unit))
                {
                    team2InArea.Add(unit);
                }
            }
        }

        progress += (0 - team1InArea.Count + team2InArea.Count) * captureRate * Time.deltaTime;
        progress = Mathf.Clamp(progress, -1f, 1f);

        //Debug.Log($"progress: {progress}");

        if (progress <= -1f)
        {
            Capture(PlayerTeam.Offensive);
            //Debug.Log("Offence captured objective");
        }
        if(progress >= 1f)
        {
            Capture(PlayerTeam.Defensive);
            //Debug.Log("Defence captured objective");
        }
    }
}

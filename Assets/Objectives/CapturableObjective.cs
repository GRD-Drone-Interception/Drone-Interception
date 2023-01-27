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

    [SerializeField] private float progress;
    private float captureRate = 0.1f;

    [SerializeField] private GameObject captureArea;

    public delegate void CaptureObjective(PlayerTeam team);
    public static event CaptureObjective OnCaptured;

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

        if (controllingTeam == PlayerTeam.Defensive && progress <= -1f)
        {
            Capture(PlayerTeam.Offensive);
            OnCaptured?.Invoke(PlayerTeam.Offensive);
            //Debug.Log("Offence captured objective");
        }
        if(controllingTeam == PlayerTeam.Offensive && progress >= 1f)
        {
            Capture(PlayerTeam.Defensive);
            OnCaptured?.Invoke(PlayerTeam.Defensive);
            //Debug.Log("Defence captured objective");
        }
    }
}

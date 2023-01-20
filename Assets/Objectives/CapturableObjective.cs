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

    private float progress;


    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        DroneUnit drone = other.GetComponent<DroneUnit>();

        if (drone != null)
        {
            if (drone.Team != controllingTeam)
            {
                attackersOnPoint++;
            }
            else
            {
                defendersOnPoint++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DroneUnit drone = other.GetComponent<DroneUnit>();

        if (drone != null)
        {
            if (drone.Team != controllingTeam)
            {
                attackersOnPoint--;
            }
            else
            {
                defendersOnPoint--;
            }
        }
    }

    public void Capture(PlayerTeam capturingTeam)
    {
        captureState = ObjetiveCaptureState.Captured;
        controllingTeam = capturingTeam;
    }

    void Update()
    {
        
    }
}

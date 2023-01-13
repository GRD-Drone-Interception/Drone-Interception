using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CamState
{
    DEFAULT,
    OVERVIEW,
    CLOSEUP
}

public class CinemachineCamManager : MonoBehaviour
{
    public static CinemachineCamManager Instance { get; private set; }

    [SerializeField] private List<CinemachineVirtualCamera> cameraList = new List<CinemachineVirtualCamera>();

    public CamState camState;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            Debug.LogWarning($"There should only be one instance of {Instance.GetType()}");
        }

        SetupCameras();
    }

    void Start()
    {

    }

    private void SetupCameras()
    {
        for (int i = 0; i < cameraList.Capacity; i++)
        {
            if (i == 0)
            {
                cameraList[i].Priority = 1;
            }
            else
            {
                cameraList[i].Priority = 0;
            }
        }
    }

    private void SetActiveCamera(CinemachineVirtualCamera activeCam)
    {
        foreach (var cam in cameraList)
        {
            cam.Priority = 0;
        }

        activeCam.Priority = 1;
    }


    void SetCamera()
    {
        SetActiveCamera(cameraList[(int)camState]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineCamManager : MonoBehaviour
{
    public static CinemachineCamManager Instance { get; private set; }

    [Tooltip("A list of virtual cameras in the scene. Element 0 will take the highest priority. Should be in the same order as Gamemanager.Gamestate")]
    [SerializeField] private List<CinemachineVirtualCamera> cameraList = new List<CinemachineVirtualCamera>();

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

    /// <summary>
    /// Prioritises the virtual camera with same index as the state parameter
    /// </summary>
    /// <param name="state"> The state to activate the associated camera </param>
    public void SetCameraState(GameState state)
    {
        SetActiveCamera(cameraList[(int)state]);
    }
}

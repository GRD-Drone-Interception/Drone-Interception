using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamManager : MonoBehaviour
{
    public static CamManager Instance { get; private set; }

    public GameObject gameCamRig;
    public GameObject mapCamRig;

    /*[Tooltip("A list of virtual cameras in the scene. Element 0 will take the highest priority. Should be in the same order as Gamemanager.Gamestate")]
    [SerializeField] private List<CinemachineVirtualCamera> cameraList = new List<CinemachineVirtualCamera>();*/

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
    }

    void Start()
    {

    }

    public void EnableGameCam()
    {
        mapCamRig.SetActive(false);
        gameCamRig.SetActive(true);
    }

    public void EnableMapCam()
    {
        gameCamRig.SetActive(false);
        mapCamRig.SetActive(true);
    }
}

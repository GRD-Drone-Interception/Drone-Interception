using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRigManager : MonoBehaviour
{
    public static CameraRigManager Instance { get; private set; }

    [SerializeField] private GameObject gameCamRig;
    [SerializeField] private GameObject mapCamRig;

    public Camera activeCamera;

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
        EnableGameCam();
    }

    void Start()
    {
        
    }

    public void EnableGameCam()
    {
        mapCamRig.SetActive(false);
        gameCamRig.SetActive(true);
        SetActiveCam(gameCamRig);
    }

    public void EnableMapCam()
    {
        gameCamRig.SetActive(false);
        mapCamRig.SetActive(true);
        SetActiveCam(mapCamRig);
    }

    private void SetActiveCam(GameObject rig)
    {
        activeCamera = rig.transform.GetChild(0).GetComponent<Camera>();
    }
}

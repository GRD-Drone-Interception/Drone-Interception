using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject gameUI;
    public GameObject GameUI => gameUI;


    [SerializeField] private GameObject deploymentUI;
    public GameObject DeploymentUI => deploymentUI;


    [SerializeField] private GameObject mapUI;
    public GameObject MapUI => mapUI;

    private List<GameObject> uiContainers;
    private GameObject activeUI;

    private void Awake()
    {
        if (Instance == null)
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
        SetActiveUI(gameUI);
    }

    public void SetActiveUI(GameObject uiContainer)
    {
        if(activeUI != null)
        {
            activeUI.SetActive(false);
        }
        activeUI = uiContainer;
        activeUI.SetActive(true);
    }

    public GameObject GetActiveUI()
    {
        return activeUI;
    }

    void Update()
    {
        
    }
}

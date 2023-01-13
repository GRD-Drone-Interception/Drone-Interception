using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TacticalMap : MonoBehaviour
{
    public static TacticalMap Instance { get; private set; }

    [SerializeField] private GameObject UnitPrefab;
    [SerializeField] private LayerMask groundLayer;

    public List<GameObject> units = new List<GameObject>();
    private int currentlySelectedUnitID = -1;

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

    private void Start()
    {

    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && GameManager.Instance.gamesate == GameState.MAP)
        {
            PlaceUnit();
        }
    }

    public void SelectUnit(int unitID)
    {
        if(unitID <= units.Count)
        {
            currentlySelectedUnitID = unitID;
        }
    }

    private void PlaceUnit()
    {
        if (EventSystem.current.IsPointerOverGameObject())    // is the touch on the GUI
        {
            return;
        }

        if(currentlySelectedUnitID < 0 || currentlySelectedUnitID > units.Count)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Debug.Log($"Raycast hit {hit.point}");
            Instantiate(units[currentlySelectedUnitID], hit.point, Quaternion.identity, hit.transform);
        }
    }
}

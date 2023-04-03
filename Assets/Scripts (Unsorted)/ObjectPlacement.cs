using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPlacement : MonoBehaviour
{
    [SerializeField] private GameObject CurrentBlueprint;
    [SerializeField] private GameObject prefabInstance;

    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        if (prefabInstance != null)
        {
            Ray ray = CameraRigManager.Instance.activeCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                prefabInstance.transform.position = hit.point;
            }

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Instantiate(CurrentBlueprint.GetComponent<Blueprint>().prefab, prefabInstance.transform.position, prefabInstance.transform.rotation);
            }
        }
    }

    public void SetBlueprint(GameObject blueprintPrefab)
    {
        if(prefabInstance != null)
        {
            Destroy(prefabInstance);
        }
        CurrentBlueprint = blueprintPrefab;
        prefabInstance = Instantiate(CurrentBlueprint);

        Ray ray = CameraRigManager.Instance.activeCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
           prefabInstance.transform.position = hit.point;
        }
    }
}

using DroneLoadout;
using UnityEngine;

public class DroneSetupMenuDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject menuNavigationBar;
    
    private void OnEnable() => WorkshopModeController.OnModeChange += SetVisibilityOfMenuNavBarOnModeChange;
    private void OnDisable() => WorkshopModeController.OnModeChange -= SetVisibilityOfMenuNavBarOnModeChange;

    private void SetVisibilityOfMenuNavBarOnModeChange(WorkshopModeController.WorkshopMode mode)
    {
        switch (mode)
        {
            case WorkshopModeController.WorkshopMode.Edit:
                menuNavigationBar.SetActive(false);
                break;
            case WorkshopModeController.WorkshopMode.Display:
                menuNavigationBar.SetActive(true);
                break;
        }
    }
}

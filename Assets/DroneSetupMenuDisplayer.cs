using DroneLoadout;
using UnityEngine;

public class DroneSetupMenuDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject menuNavigationBar;
    
    private void OnEnable() => WorkshopModeController.OnModeChange += SetVisibilityOfMenuNavBarOnModeChange;
    private void OnDisable() => WorkshopModeController.OnModeChange -= SetVisibilityOfMenuNavBarOnModeChange;

    private void SetVisibilityOfMenuNavBarOnModeChange(WorkshopMode mode)
    {
        switch (mode)
        {
            case WorkshopMode.Edit:
                menuNavigationBar.SetActive(false);
                break;
            case WorkshopMode.Display:
                menuNavigationBar.SetActive(true);
                break;
        }
    }
}

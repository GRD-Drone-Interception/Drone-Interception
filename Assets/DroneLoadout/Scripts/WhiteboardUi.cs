using UnityEngine;

namespace DroneLoadout.Scripts
{
    public class WhiteboardUi : MonoBehaviour
    {
        [SerializeField] private GameObject whiteboardCanvas; 
        [SerializeField] private GameObject miniWhiteboardPanel;
        
        private void OnEnable()
        {
            WorkshopModeController.OnModeChange += UpdateMiniWhiteboardVisibility_OnWorkshopModeChanged;
            DroneSetupMenu.OnStateChanged += UpdateWhiteboardVisibility_OnStateChanged;
        }

        private void OnDisable()
        {
            WorkshopModeController.OnModeChange -= UpdateMiniWhiteboardVisibility_OnWorkshopModeChanged;
            DroneSetupMenu.OnStateChanged -= UpdateWhiteboardVisibility_OnStateChanged;
        }

        private void Start() => miniWhiteboardPanel.gameObject.SetActive(false);

        private void UpdateWhiteboardVisibility_OnStateChanged(DroneSetupMenuStates state)
        {
            whiteboardCanvas.SetActive(state == DroneSetupMenuStates.Workshop);
        }
        
        private void UpdateMiniWhiteboardVisibility_OnWorkshopModeChanged(WorkshopModeController.WorkshopMode mode)
        {
            switch (mode)
            {
                case WorkshopModeController.WorkshopMode.Display:
                    miniWhiteboardPanel.SetActive(false);
                    break;
                case WorkshopModeController.WorkshopMode.Edit:
                    miniWhiteboardPanel.SetActive(true);
                    break;
            }
        }
    }
}
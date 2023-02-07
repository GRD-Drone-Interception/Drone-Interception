using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Drones
{
    public class DroneClassUi : MonoBehaviour // Combine this class with DroneAttachmentUi?
    {
        //[SerializeField] private List<DroneCreator> droneCreators;

        private void OnEnable()
        {
            DroneLoadoutCameraMode.OnModeChange += OnCameraModeChange;
            //droneCreators.ForEach(ctx => ctx.OnDroneSpawned += HideDroneSpawnButton);
        }

        private void OnDisable()
        {
            DroneLoadoutCameraMode.OnModeChange -= OnCameraModeChange;
            //droneCreators.ForEach(ctx => ctx.OnDroneSpawned -= HideDroneSpawnButton);
        }

        private void OnCameraModeChange(DroneLoadoutCameraMode.CameraMode mode)
        {
            switch (mode)
            {
                case DroneLoadoutCameraMode.CameraMode.Edit:
                {
                    //droneCreators.ForEach(ctx => ctx.gameObject.SetActive(false));
                    break;
                }
                case DroneLoadoutCameraMode.CameraMode.Display:
                {
                    //droneCreators.ForEach(ctx => ctx.gameObject.SetActive(true));
                    break;
                }
            }
        }
    
        private void HideDroneSpawnButton()
        {
            StartCoroutine(HideDroneSpawnButtonCoroutine());
        }

        private IEnumerator HideDroneSpawnButtonCoroutine()
        {
            //droneCreators.ForEach(ctx => ctx.GetComponent<Image>().color = new Color(0.2f,0f,0f,0.8f));
            //droneCreators.ForEach(ctx => ctx.SetActive(false));
            yield return new WaitForSeconds(1.0f);
            //droneCreators.ForEach(ctx => ctx.GetComponent<Image>().color = new Color(0,0,0,0.8f));
            //droneCreators.ForEach(ctx => ctx.SetActive(true));
        }
    }
}

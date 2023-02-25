using System.IO;
using DroneLoadout;
using UnityEditor;
using UnityEngine;

namespace Testing
{
    public class DroneSpawner : MonoBehaviour
    {
        //[SerializeField] private DroneType droneType;
        //[SerializeField] private GameObject dronePrefab;
        //[SerializeField] private GameObject propellerAttachmentPrefab;

        private void Start()
        {
            SpawnDrone();
        }

        private void SpawnDrone()
        {
            string droneName = "TestDrone";
            string path = $"Assets/Art/Prefabs/Drone/Class/Custom/{droneName}.prefab";

            if (File.Exists(path))
            {
                GameObject customisedDronePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                GameObject customisedDrone = Instantiate(customisedDronePrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Prefab does not exist!");
            }



            //var droneObject = Instantiate(dronePrefab, transform.position, Quaternion.identity);
            //var drone = droneObject.GetComponent<Drone>();
            
            // Instantiate drone attachment prefabs
            /*foreach (var attachmentPoint in drone.GetAttachmentPoints())
            {
                var propellerAttachment = Instantiate(propellerAttachmentPrefab).GetComponent<DroneAttachment>();
                drone.Decorate(propellerAttachment, attachmentPoint);
                Debug.Log($"{attachmentPoint.name}: {attachmentPoint.GetDroneAttachment()}");
                
                //attachmentPoint.AddDroneAttachment();
                //attachmentPoint.GetDroneAttachment()
                //drone.Decorate(, attachmentPoint);
            }*/
            
            //DroneSaveSystem.LoadFromFile();
            //drone.DecorableDrone = DroneFactory.CreateDrone(drone.DroneConfigData.droneType, drone.DroneConfigData);
            //_decorableDrone = new DroneDecorator(_decorableDrone, droneAttachment.Data);

            //string customisationData = PlayerPrefs.GetString("DroneCustomisationData");
            //IDrone customisedDrone = JsonUtility.FromJson<IDrone>(customisationData);
            //GameObject newDrone = Instantiate(dronePrefab, transform.position, Quaternion.identity);
            //newDrone.GetComponent<Drone>().Decorate(customisedDrone, );
            //Debug.Log(customisationData);
        }
    }
}

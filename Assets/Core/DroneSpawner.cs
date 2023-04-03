using System.IO;
using DroneBehaviours.Scripts;
using DroneLoadout;
using DroneLoadout.Scripts;
using UnityEditor;
using UnityEngine;

namespace Core
{
    public class DroneSpawner : MonoBehaviour
    {
        //[SerializeField] private DroneType droneType;
        [SerializeField] private PlayerTeam playerTeam;
        [SerializeField] private string dronePrefabPath = "Assets/Resources/";

        private void Start()
        {
            SpawnDrone();
        }

        private void SpawnDrone()
        {
            string droneName = "TestDrone.prefab";
            var path = dronePrefabPath + droneName;

            if (File.Exists(path))
            {
                GameObject customisedDronePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                GameObject customisedDrone = Instantiate(customisedDronePrefab);
                customisedDrone.transform.SetPositionAndRotation(transform.position, transform.rotation);
                var drone = customisedDrone.GetComponent<Drone>();
                drone.SetTeam(playerTeam);
                DroneManager.AddDrone(drone);
                Debug.Log($"Drone Cost: {drone.DecorableDrone.Cost}");
                // TODO: Overwrite/create a new ScriptableObject DroneConfig?
            }
            else
            {
                Debug.LogError("Prefab does not exist!");
            }
        }
    }
}

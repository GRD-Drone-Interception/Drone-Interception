using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DroneLoadout;
using UnityEngine;

namespace Testing
{
    public class DroneSaveSystem
    {
        public static void SaveToFile(IDrone drone)
        {
            // Create a file to save the drone data to
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/droneData.dat");

            // Assemble the drone data
            DroneData data = new DroneData(); // Use IDrone?
            data.range = drone.Range;

            bf.Serialize(file, data);
            file.Close();
        }

        public static void LoadFromFile(IDrone drone)
        {
            if (File.Exists(Application.persistentDataPath + "/droneData.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/droneData.dat", FileMode.Open);
                DroneData data = (DroneData) bf.Deserialize(file);
                file.Close();

                //drone.Range = data.range;
            }
        }
    }
}
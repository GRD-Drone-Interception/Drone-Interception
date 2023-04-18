using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class JsonFileHandlerEditor : EditorWindow {

        [MenuItem("Tools/Delete Saved Data")]
        public static void ShowWindow()
        {
            GetWindow(typeof(JsonFileHandlerEditor));
        }

        private void OnGUI()
        {
            GUILayout.Label("Delete Saved Data", EditorStyles.boldLabel);
            GUILayout.Space(10);
            
            GUILayout.Label("This will delete any saved (JSON) data located at the persistent data path\n" +
                            "such as budget, drone, and post analytic data!");
            GUILayout.Space(10);

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Delete ALL Saved Data?")) 
            {
                string path = Application.persistentDataPath;
                DirectoryInfo directory = new DirectoryInfo(path);

                if (directory.Exists) 
                {
                    FileInfo[] files = directory.GetFiles("*.json");
                    foreach (FileInfo file in files)
                    {
                        file.Delete();
                    }

                    Debug.Log("All JSON files deleted.");
                } 
                else 
                {
                    Debug.LogWarning("Saved data directory does not exist.");
                }
            }
            GUI.backgroundColor = Color.white;

            if (GUILayout.Button("Close"))
            {
                Close();
            }
        }
    }
}
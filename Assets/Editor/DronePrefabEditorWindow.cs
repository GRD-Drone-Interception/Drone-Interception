using DroneLoadout.Decorators;
using DroneLoadout.Factory;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class DronePrefabEditorWindow : EditorWindow
    {
        public DroneConfigData droneClassData;

        [MenuItem("Tools/Drone Prefab Editor")]
        public static void ShowWindow()
        {
            var window = GetWindow<DronePrefabEditorWindow>("Drone Prefab Editor");
            var contentRect = new Rect(500, 400, 600, 500);
            window.position = contentRect;
        }

        private void OnGUI()
        {
            GUILayout.Label("Drone Prefab Editor", EditorStyles.boldLabel);

            EditorGUILayout.Space();
        
            droneClassData = (DroneConfigData)EditorGUILayout.ObjectField("Drone Prefab", droneClassData, typeof(DroneConfigData), false);

            if (droneClassData == null)
            {
                EditorGUILayout.HelpBox("Please select a drone prefab to continue.", MessageType.Warning);
                return;
            }

            EditorGUILayout.Space();
        
            Texture2D texture = AssetPreview.GetAssetPreview(droneClassData.dronePrefab);
            GUILayout.Label("", GUILayout.Height(128), GUILayout.Width(128));
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            GUILayout.Label("New Stats", EditorStyles.boldLabel);

            string droneName = "";
            DroneType? droneType = DroneType.Quadcopter;
            string desc = "";
            float speed = 0;
            float acceleration = 0;
            float range = 0;
            float weight = 0;
            float cost = 0;
        
            var newDroneName = EditorGUILayout.TextField("Name",droneName);
            EditorGUILayout.EnumPopup("Type", droneType);
            EditorGUILayout.TextField("Description", desc);
            EditorGUILayout.FloatField("Speed", speed);
            EditorGUILayout.FloatField("Acceleration", acceleration);
            EditorGUILayout.FloatField("Range", range);
            EditorGUILayout.FloatField("Weight", weight);
            EditorGUILayout.FloatField("Cost", cost);
            EditorGUILayout.EndVertical();
        
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Current Stats", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"Name: {droneClassData.droneName}");
            EditorGUILayout.LabelField($"Type: {droneClassData.droneType}");
            EditorGUILayout.LabelField($"Description: {droneClassData.droneDescription}");
            EditorGUILayout.LabelField($"Speed: {droneClassData.TopSpeed}");
            EditorGUILayout.LabelField($"Acceleration: {droneClassData.Acceleration}");
            EditorGUILayout.LabelField($"Range: {droneClassData.Range}");
            EditorGUILayout.LabelField($"Weight: {droneClassData.Weight}");
            EditorGUILayout.LabelField($"Cost: {droneClassData.Cost}");
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            //float speed = EditorGUILayout.Slider(droneClassData.Speed, 0f, 500f);
            //dronePrefab.GetComponent<Drone>().speed = speed;
        }

    }
}

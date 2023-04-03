using DroneLoadout.Decorators;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(DroneConfigData))]
    public class DroneConfigPreviewEditor : UnityEditor.Editor
    {
        DroneConfigData _droneConfigData;
    
        private void OnEnable()
        {
            _droneConfigData = target as DroneConfigData;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
      
            if (_droneConfigData.dronePrefab == null)
                return;
        
            GUILayout.Space(10);
        
            Texture2D texture = AssetPreview.GetAssetPreview(_droneConfigData.dronePrefab);
            GUILayout.Label("", GUILayout.Height(128), GUILayout.Width(128));
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
        }
    }
}
using DroneLoadout.Decorators;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(DroneAttachmentData))]
    public class DroneAttachmentPreviewEditor : UnityEditor.Editor
    {
        DroneAttachmentData _droneAttachmentData;
    
        private void OnEnable()
        {
            _droneAttachmentData = target as DroneAttachmentData;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.BeginHorizontal();
            if (_droneAttachmentData.Prefab != null)
            {
                GUILayout.Space(10);
                GUILayout.BeginVertical();
                GUILayout.Label("Prefab Preview");
                Texture2D attachmentPrefabTexture = AssetPreview.GetAssetPreview(_droneAttachmentData.Prefab);
                GUILayout.Label("", GUILayout.Height(128), GUILayout.Width(128));
                GUI.DrawTexture(GUILayoutUtility.GetLastRect(), attachmentPrefabTexture);
                GUILayout.EndVertical();
            }

            if (_droneAttachmentData.PrefabSprite != null)
            {
                GUILayout.Space(10);
                GUILayout.BeginVertical();
                GUILayout.Label("Sprite Preview");
                Texture2D attachmentSpriteTexture = AssetPreview.GetAssetPreview(_droneAttachmentData.PrefabSprite);
                GUILayout.Label("", GUILayout.Height(128), GUILayout.Width(128));
                GUI.DrawTexture(GUILayoutUtility.GetLastRect(), attachmentSpriteTexture);
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
        }
    }
}
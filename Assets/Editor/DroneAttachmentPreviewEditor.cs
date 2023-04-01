using DroneLoadout.Decorators;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DroneAttachmentData))]
public class DroneAttachmentPreviewEditor : Editor
{
    DroneAttachmentData _droneAttachmentData;
    
    private void OnEnable()
    {
        _droneAttachmentData = target as DroneAttachmentData;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
      
        if (_droneAttachmentData.attachmentPrefab == null)
            return;
        
        GUILayout.Space(10);
        
        Texture2D texture = AssetPreview.GetAssetPreview(_droneAttachmentData.attachmentPrefab);
        GUILayout.Label("", GUILayout.Height(128), GUILayout.Width(128));
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
    }
}
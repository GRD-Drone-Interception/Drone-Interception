using UnityEditor;
using UnityEngine;

public class UpgradeManagerWindow : EditorWindow
{
    [MenuItem("Tools/Upgrade Editor")]
    public static void Open()
    {
        GetWindow<UpgradeManagerWindow>();
    }

    public Transform upgradeRoot;
    public Material lineMaterial;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);
        //EditorGUILayout.PropertyField();

        EditorGUILayout.PropertyField(obj.FindProperty("upgradeRoot"));
        if(upgradeRoot == null)
        {
            EditorGUILayout.HelpBox("Root upgrade must be selected. Please assign a root upgrade", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }
        obj.ApplyModifiedProperties();
    }

    void DrawButtons()
    {
        if (GUILayout.Button("Create Upgrade"))
        {
            CreateUpgrade();
        }
    }

    void CreateUpgrade()
    {
        GameObject upgradeObject = new GameObject("Upgrade " + (upgradeRoot.childCount + 1), typeof(Upgrade));
        upgradeObject.transform.SetParent(upgradeRoot, false);

        Upgrade upgrade = upgradeObject.GetComponent<Upgrade>();
        
        if(upgradeRoot.childCount > 1)
        {
            upgrade.previousUpgrade = upgradeRoot.GetChild(upgradeRoot.childCount - 2).GetComponent<Upgrade>();
            upgrade.previousUpgrade.nextUpgrade = upgrade;
        }

        Selection.activeGameObject = upgrade.gameObject;
    }
}

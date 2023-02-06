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
        if(Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Upgrade>())
        {
            if (GUILayout.Button("Create Previous Upgrade"))
            {
                CreatePreviousUpgrade();
            }
            if (GUILayout.Button("Create Next Upgrade"))
            {
                CreateNextUpgrade();
            }
            if (GUILayout.Button("Remove Upgrade"))
            {
                RemoveUpgrade();
            }
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

    void CreatePreviousUpgrade()
    {
        GameObject upgradeObject = new GameObject("Upgrade " + upgradeRoot.childCount, typeof(Upgrade));
        upgradeObject.transform.SetParent(upgradeRoot, false);

        Upgrade newUpgrade = upgradeObject.GetComponent<Upgrade>();

        Upgrade selectedUpgrade = Selection.activeGameObject.GetComponent<Upgrade>();

        if(selectedUpgrade.previousUpgrade != null)
        {
            newUpgrade.previousUpgrade = selectedUpgrade.previousUpgrade;
            selectedUpgrade.previousUpgrade.nextUpgrade = newUpgrade;
        }

        newUpgrade.nextUpgrade = selectedUpgrade;

        selectedUpgrade.previousUpgrade = newUpgrade;

        newUpgrade.transform.SetSiblingIndex(selectedUpgrade.transform.GetSiblingIndex());

        Selection.activeGameObject = newUpgrade.gameObject;
    }

    void CreateNextUpgrade()
    {
        GameObject upgradeObject = new GameObject("Upgrade " + upgradeRoot.childCount, typeof(Upgrade));
        upgradeObject.transform.SetParent(upgradeRoot, false);

        Upgrade newUpgrade = upgradeObject.GetComponent<Upgrade>();

        Upgrade selectedUpgrade = Selection.activeGameObject.GetComponent<Upgrade>();

        newUpgrade.previousUpgrade = selectedUpgrade;

        if(selectedUpgrade.nextUpgrade != null)
        {
            selectedUpgrade.nextUpgrade.previousUpgrade = newUpgrade;
            newUpgrade.nextUpgrade = selectedUpgrade.nextUpgrade;
        }

        selectedUpgrade.nextUpgrade = newUpgrade;

        newUpgrade.transform.SetSiblingIndex(selectedUpgrade.transform.GetSiblingIndex() + 1);

        Selection.activeGameObject = newUpgrade.gameObject;
    }

    void RemoveUpgrade()
    {
        Upgrade selectedUpgrade = Selection.activeGameObject.GetComponent<Upgrade>();

        if(selectedUpgrade.nextUpgrade != null)
        {
            selectedUpgrade.nextUpgrade.previousUpgrade = selectedUpgrade.previousUpgrade;
        }
        if (selectedUpgrade.previousUpgrade != null)
        {
            selectedUpgrade.previousUpgrade.nextUpgrade = selectedUpgrade.nextUpgrade;
            Selection.activeGameObject = selectedUpgrade.previousUpgrade.gameObject;
        }

        DestroyImmediate(selectedUpgrade.gameObject);
    }
}

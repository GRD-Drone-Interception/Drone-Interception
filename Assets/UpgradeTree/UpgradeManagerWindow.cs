using UnityEditor;
using UnityEngine;
using TMPro;

public class UpgradeManagerWindow : EditorWindow
{
    [MenuItem("Tools/Upgrade Editor")]
    public static void Open()
    {
        GetWindow<UpgradeManagerWindow>();
    }

    public Transform upgradeRoot;
    public GameObject upgradePrefab;
    public Material lineMaterial;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);
        //EditorGUILayout.PropertyField();

        EditorGUILayout.PropertyField(obj.FindProperty("upgradeRoot"));
        EditorGUILayout.PropertyField(obj.FindProperty("upgradePrefab"));

        if (upgradeRoot == null)
        {
            EditorGUILayout.HelpBox("Root upgrade must be selected. Please assign a root upgrade", MessageType.Warning);
        }
        else if (upgradePrefab == null)
        {
            EditorGUILayout.HelpBox("An upgrade prefab must be selected. Please assign an upgrade prefab", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    /// <summary>
    /// Draws buttons to editor window
    /// </summary>
    void DrawButtons()
    {
        if (GUILayout.Button("Create New Upgrade"))
        {
            CreateNewUpgrade();
        }
        if(Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Upgrade>())
        {
            if(GUILayout.Button("Create Branched Upgrade"))
            {
                CreateBranch();
            }
            if (GUILayout.Button("Create Upgrade Before"))
            {
                CreatePreviousUpgrade();
            }
            if (GUILayout.Button("Create Upgrade After"))
            {
                CreateNextUpgrade();
            }
            if (GUILayout.Button("Remove Upgrade"))
            {
                RemoveUpgrade();
            }
        }

        GUI.Label(new Rect(10, 40, 100, 40), GUI.tooltip);
    }

    /// <summary>
    /// Creates a new upgrade at newest level of the root
    /// </summary>
    void CreateNewUpgrade()
    {
        //GameObject upgradeObject = new GameObject("Upgrade " + (upgradeRoot.childCount + 1), typeof(Upgrade));
        GameObject upgradeObject = Instantiate(upgradePrefab, upgradeRoot, false);
        upgradeObject.name = "Upgrade " + (upgradeRoot.childCount);
        upgradeObject.transform.GetChild(0).GetComponent<TMP_Text>().text = upgradeRoot.childCount.ToString();

        Upgrade upgrade = upgradeObject.GetComponent<Upgrade>();
        
        if(upgradeRoot.childCount > 1)
        {
            upgrade.previousUpgrade = upgradeRoot.GetChild(upgradeRoot.childCount - 2).GetComponent<Upgrade>();
            upgrade.previousUpgrade.nextUpgrade = upgrade;
        }

        Selection.activeGameObject = upgrade.gameObject;
    }

    /// <summary>
    /// Creates a new upgrade before the selected upgrade object
    /// </summary>
    void CreatePreviousUpgrade()
    {
        GameObject upgradeObject = Instantiate(upgradePrefab, upgradeRoot, false);
        upgradeObject.name = "Upgrade " + (upgradeRoot.childCount);
        upgradeObject.transform.GetChild(0).GetComponent<TMP_Text>().text = upgradeRoot.childCount.ToString();

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

    /// <summary>
    /// Creates a new upgrade after the selected upgrade object
    /// </summary>
    void CreateNextUpgrade()
    {
        GameObject upgradeObject = Instantiate(upgradePrefab, upgradeRoot, false);

        Upgrade newUpgrade = upgradeObject.GetComponent<Upgrade>();
        upgradeObject.name = "Upgrade " + (upgradeRoot.childCount);
        upgradeObject.transform.GetChild(0).GetComponent<TMP_Text>().text = upgradeRoot.childCount.ToString();

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

    /// <summary>
    /// Deletes the currently seleceted upgrade
    /// </summary>
    void RemoveUpgrade()
    {
        Upgrade selectedUpgrade = Selection.activeGameObject.GetComponent<Upgrade>();

        if(selectedUpgrade.nextUpgrade != null)
        {
            selectedUpgrade.nextUpgrade.previousUpgrade = selectedUpgrade.previousUpgrade;
        }
        if (selectedUpgrade.previousUpgrade != null)
        {
            if (selectedUpgrade.previousUpgrade.branchUpgrades.Contains(selectedUpgrade))
            {
                selectedUpgrade.previousUpgrade.branchUpgrades.Remove(selectedUpgrade);
            }
            else
            {
                selectedUpgrade.previousUpgrade.nextUpgrade = selectedUpgrade.nextUpgrade;
                Selection.activeGameObject = selectedUpgrade.previousUpgrade.gameObject;
            }
        }

        DestroyImmediate(selectedUpgrade.gameObject);
    }

    void CreateBranch()
    {
        GameObject upgradeObject = Instantiate(upgradePrefab, upgradeRoot, false);

        Upgrade upgrade = upgradeObject.GetComponent<Upgrade>();
        upgradeObject.name = "Upgrade " + (upgradeRoot.childCount);
        upgradeObject.transform.GetChild(0).GetComponent<TMP_Text>().text = upgradeRoot.childCount.ToString();

        Upgrade branchedFrom = Selection.activeGameObject.GetComponent<Upgrade>();
        branchedFrom.branchUpgrades.Add(upgrade);

        upgrade.previousUpgrade = branchedFrom;
        upgrade.transform.SetSiblingIndex(branchedFrom.transform.GetSiblingIndex() + 1);

        Selection.activeGameObject = upgrade.gameObject;
    }
}

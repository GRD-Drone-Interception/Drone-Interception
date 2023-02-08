using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Upgrade previousUpgrade;
    public Upgrade nextUpgrade;
    public List<Upgrade> branchUpgrades = new List<Upgrade>();

    [SerializeField] private float lineWidth = 0.01f;
    [SerializeField] private Material lineMaterial;

    Button button;

    public bool unlocked = false;
    public bool purchased = false;

    private void Awake()
    {
        this.gameObject.AddComponent<LineRenderer>();
        LineRenderer lr = this.GetComponent<LineRenderer>();

        lr.material = lineMaterial;

        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;

        if (previousUpgrade != null)
        {
            lr.SetPosition(0, this.transform.position);
            lr.SetPosition(1, previousUpgrade.transform.position);
        }

        lr.sortingOrder = -1;

        button = this.GetComponent<Button>();
        button.interactable = false;

        if (previousUpgrade == null)
        {
            Unlock();
        }
    }

    void Start()
    {

    }

    public void Purchase()
    {
        if (purchased)
            return;

        Debug.Log(this.gameObject.name + " puchased");

        if (nextUpgrade != null)
        {
            nextUpgrade.Unlock();
        }
        foreach (Upgrade branchedUpgrade in branchUpgrades)
        {
            branchedUpgrade.Unlock();
        }

        purchased = true;
        button.interactable = false;
    }

    public void Unlock()
    {
        unlocked = true;
        button.interactable = true;
    }

    void Update()
    {

    }
}

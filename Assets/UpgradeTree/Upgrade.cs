using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public Upgrade previousUpgrade;
    public Upgrade nextUpgrade;

    [SerializeField] private float lineWidth = 0.01f;
    [SerializeField] private Material lineMaterial;

    void Start()
    {
        this.gameObject.AddComponent<LineRenderer>();
        LineRenderer lr = this.GetComponent<LineRenderer>();

        lr.material = lineMaterial;

        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;

        if (previousUpgrade == null)
            return;

        lr.SetPosition(0, this.transform.position);
        lr.SetPosition(1, previousUpgrade.transform.position);

        lr.sortingOrder = -1;
    }

    void Update()
    {

    }
}

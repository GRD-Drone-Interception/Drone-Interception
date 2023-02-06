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
        /*for (int i = 0; i < nextUpgrade.Count; i++)
        {
            GameObject childPos = new GameObject("Upgrade Connector");
            childPos.transform.parent = this.transform;
            childPos.transform.position = nextUpgrade[i].transform.position;
            childPos.AddComponent<LineRenderer>();
            
            LineRenderer lr = childPos.GetComponent<LineRenderer>();

            lr.material = lineMaterial;

            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;

            lr.SetPosition(0, this.transform.position);
            lr.SetPosition(1, nextUpgrade[i].transform.position);

            lr.sortingOrder = -1;
        }*/
    }

    void Update()
    {

    }
}

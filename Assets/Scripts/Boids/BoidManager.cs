using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public GameObject boidPrefab;
    public int numBoids = 10;
    public float spawnRadius = 10f;

    public List<Boid> boids = new List<Boid>();

    private void Start()
    {
        for (int i = 0; i < numBoids; i++)
        {
            Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
            Quaternion rot = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            GameObject boidObj = Instantiate(boidPrefab, pos, rot);
            boids.Add(boidObj.GetComponent<Boid>());
        }
    }
}

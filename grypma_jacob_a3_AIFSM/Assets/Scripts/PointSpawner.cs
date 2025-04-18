using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    public GameObject pointPrefab;
    private GameObject currentPoint;

    void Start()
    {
        SpawnPoint();
    }

    public void SpawnPoint()
    {
        if (currentPoint != null)
        {
            Destroy(currentPoint);
        }

        Vector2 spawnPos = GridManager.Instance.GetRandomGridPosition();
        currentPoint = Instantiate(pointPrefab, spawnPos, Quaternion.identity);
    }
}

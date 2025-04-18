using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject opponentSnakePrefab;

    void Start()
    {
        SpawnOpponent();
    }

    void SpawnOpponent()
    {
        Vector2 spawnPos = GridManager.Instance.GetRandomGridPosition();
        Instantiate(opponentSnakePrefab, spawnPos, Quaternion.identity);
    }
}

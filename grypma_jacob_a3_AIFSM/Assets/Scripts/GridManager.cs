using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script to handle grid data, including grid size, cell size, and positioinal data

public class GridManager : MonoBehaviour
{
    public static GridManager Instance; //singleton pattern

    public int width = 20;
    public int height = 20;
    public float cellSize = 1f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    //snap snake movement to grid
    public Vector2 SnapToGrid(Vector2 position)
    {
        int x = Mathf.RoundToInt(position.x / cellSize);
        int y = Mathf.RoundToInt(position.y / cellSize);
        return new Vector2(x * cellSize, y * cellSize);
    }

    //ensure we remain in bounds
    public bool IsInBounds(Vector2 position)
    {
        int x = Mathf.RoundToInt(position.x / cellSize);
        int y = Mathf.RoundToInt(position.y / cellSize);
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    //get random position for point spawning
    public Vector2 GetRandomGridPosition()
    {
        int x = Random.Range(0, width);
        int y = Random.Range(0, height);
        return new Vector2(x * cellSize, y * cellSize);
    }

    //make grid visible in editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;

        for (int x = 0; x <= width; x++)
        {
            Gizmos.DrawLine(new Vector3(x * cellSize, 0, 0), new Vector3(x * cellSize, height * cellSize, 0));
        }

        for (int y = 0; y <= height; y++)
        {
            Gizmos.DrawLine(new Vector3(0, y * cellSize, 0), new Vector3(width * cellSize, y * cellSize, 0));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectingPointsState : State
{
    private OpponentSnake opponent;

    public CollectingPointsState(OpponentSnake opponent)
    {
        this.opponent = opponent;
    }

    public override void Enter() { }

    public override void Execute()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("Point");

        if (points.Length == 0)
            return;

        GameObject closestPoint = points
            .OrderBy(p => Vector2.Distance(opponent.transform.position, p.transform.position))
            .FirstOrDefault();

        if (closestPoint == null)
            return;

        Vector2 dirToTarget = closestPoint.transform.position - opponent.transform.position;

        //Snap to the best cardinal direction
        Vector2 cardinalDir = GetBestCardinalDirection(dirToTarget, closestPoint.transform.position);

        opponent.direction = cardinalDir;
    }

    public override void Exit() { }

    private Vector2 GetBestCardinalDirection(Vector2 dirToTarget, Vector2 targetPos)
    {
        Vector2[] directions = new Vector2[]
        {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
        };

        Vector2 currentPos = opponent.transform.position;
        Vector2 bestDir = Vector2.zero;
        float closestDistance = Mathf.Infinity;

        foreach (Vector2 dir in directions)
        {
            Vector2 newPos = currentPos + dir * GridManager.Instance.cellSize;
            float distance = Vector2.Distance(newPos, targetPos);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                bestDir = dir;
            }
        }

        return bestDir;
    }
}


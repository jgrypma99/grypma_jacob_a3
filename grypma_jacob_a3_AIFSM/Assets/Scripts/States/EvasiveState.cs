using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveState : State
{
    private OpponentSnake opponent;
    private float dashDistance = 5f;

    public EvasiveState(OpponentSnake opponent)
    {
        this.opponent = opponent;
    }

    public override void Enter() { }

    public override void Execute()
    {
        // Check if the player is dashing
        if (Vector2.Distance(opponent.transform.position, GameObject.FindWithTag("Player").transform.position) <= dashDistance)
        {
            // Change direction to avoid player
            Vector2 oppositeDirection = (Vector2)opponent.transform.position - (Vector2)GameObject.FindWithTag("Player").transform.position;
            opponent.direction = DirectionUtils.SnapToCardinal(oppositeDirection);
        }
    }

    public override void Exit() { }
}

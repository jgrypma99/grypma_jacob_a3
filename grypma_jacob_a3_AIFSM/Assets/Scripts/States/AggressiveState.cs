using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveState : State
{
    private OpponentSnake opponent;
    private float detectionRadius = 5f;

    public AggressiveState(OpponentSnake opponent)
    {
        this.opponent = opponent;
    }

    public override void Enter() { }

    public override void Execute()
    {
        // Check player distance, if player is within 5 grid spaces, switch to cutting-off logic
        Vector2 playerPosition = GameObject.FindWithTag("Player").transform.position;
        if (Vector2.Distance(opponent.transform.position, playerPosition) <= detectionRadius)
        {
            // Calculate direction to cut off player (simple logic here, can be refined)
            Vector2 directionToPlayer = (playerPosition - (Vector2)opponent.transform.position).normalized;
            opponent.direction = DirectionUtils.SnapToCardinal(directionToPlayer);
        }
        else
        {
            opponent.stateMachine.ChangeState(new CollectingPointsState(opponent)); // Switch back to collecting if not in range
        }
    }

    public override void Exit() { }
}
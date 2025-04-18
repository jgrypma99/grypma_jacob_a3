using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSnake : MonoBehaviour
{
    public StateMachine stateMachine;
    public GameObject segmentPrefab;
    public float moveInterval = 0.2f;
    public float dashInterval = 0.1f;

    private List<Transform> segments = new List<Transform>();
    public Vector2 direction = Vector2.right;
    private float moveTimer;
    private bool isDashing;


    void Start()
    {
        stateMachine = new StateMachine();
        segments.Add(this.transform);
        Grow(); Grow(); //start with 3 segments
        stateMachine.ChangeState(new CollectingPointsState(this)); //set to search for points initially
    }

    void Update()
    {
        HandleStateTransitions();
        stateMachine.Update();
    }

    void FixedUpdate()
    {
        moveTimer += Time.fixedDeltaTime;
        float interval = isDashing ? dashInterval : moveInterval;

        if (moveTimer >= interval)
        {
            moveTimer = 0f;
            Move();
        }
    }

    void Move()
    {
        Vector2 targetPosition = (Vector2)transform.position + direction * GridManager.Instance.cellSize;

        //Check self-collision
        foreach (Transform segment in segments)
        {
            if (segment == this.transform) continue; // skip head
            if ((Vector2)segment.position == targetPosition)
            {
                Debug.Log("Self-collision! Game Over.");;
                return;
            }
        }

        //Check if target is in bounds
        if (!GridManager.Instance.IsInBounds(targetPosition))
        {
            Debug.Log("Out of bounds.");
            return;
        }

        //Move segments
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        //Move head
        transform.position = GridManager.Instance.SnapToGrid(targetPosition);
    }

    //increase size when collecting a point
    public void Grow()
    {
        GameObject segment = Instantiate(segmentPrefab);
        segment.transform.position = segments[segments.Count - 1].position;
        segments.Add(segment.transform);
    }

    //handle transitions between AI states
    void HandleStateTransitions()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        Vector2 playerPos = player.transform.position;
        float distanceToPlayer = Vector2.Distance(transform.position, playerPos);

        bool playerIsDashing = player.GetComponent<PlayerSnake>().IsDashing();

        State current = stateMachine.GetCurrentState();

        if (playerIsDashing)
        {
            if (!(current is EvasiveState && distanceToPlayer <= 5f))
            {
                stateMachine.ChangeState(new EvasiveState(this));
                Debug.Log("Evasive State");
            }
        }
        else if (distanceToPlayer <= 5f)
        {
            if (!(current is AggressiveState))
            {
                stateMachine.ChangeState(new AggressiveState(this));
                Debug.Log("Aggressive State");
            }
        }
        else
        {
            if (!(current is CollectingPointsState))
            {
                stateMachine.ChangeState(new CollectingPointsState(this));
                Debug.Log("Idle");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Point"))
        {
            Grow();

            //Tell the spawner to make a new one
            FindObjectOfType<PointSpawner>().SpawnPoint();

            Destroy(other.gameObject);

            Debug.Log("Point collected");
        }

        //Kill opponent when colliding with player segments
        if (other.CompareTag("Segment"))
        {
            
        }
    }

}

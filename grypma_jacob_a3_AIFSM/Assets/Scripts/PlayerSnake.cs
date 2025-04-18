using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSnake : MonoBehaviour
{
    public GameObject segmentPrefab;
    public float moveInterval = 0.2f;
    public float dashInterval = 0.1f;

    private List<Transform> segments = new List<Transform>();
    private Vector2 direction = Vector2.right;
    private float moveTimer;
    private bool isDashing;

    void Start()
    {
        //Snap initial position to center of grid
        Vector2 center = new Vector2(
            GridManager.Instance.width / 2 * GridManager.Instance.cellSize,
            GridManager.Instance.height / 2 * GridManager.Instance.cellSize
        );
        transform.position = GridManager.Instance.SnapToGrid(center);

        segments.Add(this.transform);
        Grow(); Grow(); //start with 3 segments
    }

    void Update()
    {
        //Direction input (restrict to orthogonal)
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
            direction = Vector2.up;
        if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
            direction = Vector2.down;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
            direction = Vector2.left;
        if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
            direction = Vector2.right;

        isDashing = Input.GetKey(KeyCode.LeftShift);
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
                Debug.Log("Self-collision! Game Over.");
                OnDeath();
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

    public void Grow()
    {
        GameObject segment = Instantiate(segmentPrefab);
        segment.transform.position = segments[segments.Count - 1].position;
        segments.Add(segment.transform);
    }

    void OnDeath()
    {
        enabled = false;
        Debug.Log("Player Died");
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
    }

    //checks if we are currently dashing
    public bool IsDashing()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
    [SerializeField]
    public float speed;

    [SerializeField]
    public float sideLength;

    [SerializeField]
    public float secondSideLength;

    public bool firstRota = true;

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private List<Vector2> waypoints = new List<Vector2>();
    private int currentWaypoint = 0;
    public SpriteRenderer spriteRenderer;

    private bool followingTarget = false; // Pour savoir si on suit une cible ponctuelle

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        startPosition = transform.position;

        // Ajoutez dynamiquement les waypoints
        waypoints.Add(startPosition);
        waypoints.Add(startPosition - Vector2.up * secondSideLength);
        waypoints.Add(waypoints[1] + Vector2.right * sideLength);
        waypoints.Add(waypoints[2] + Vector2.up * secondSideLength);

        targetPosition = waypoints[1];

        // Ajustez la rotation initiale
        if (firstRota)
        {
            Vector2 initialDirection = waypoints[1] - waypoints[0];
            float initialAngle = Mathf.Atan2(initialDirection.y, initialDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, initialAngle - 90);
            firstRota = false;
        }
    }

    public void ChangeSpeed(float setSpeed)
    {
        if (setSpeed < 0)
        {
            Debug.LogWarning("Speed cannot be negative. Setting to 0.");
            speed = 0;
        }
        else if (setSpeed > 100) // Ajustez la limite supérieure en fonction du jeu
        {
            Debug.LogWarning("Speed too high. Setting to 100.");
            speed = 100;
        }
        else
        {
            speed = setSpeed;
        }
    }

    void Update()
    {
        Vector2 direction = targetPosition - (Vector2)transform.position;

        // Ajustez la position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Ajustez la rotation pour faire face à la direction
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Ajustez l'angle selon l'orientation initiale
        }

        // Vérifiez si le point cible est atteint
        if ((Vector2)transform.position == targetPosition)
        {
            if (followingTarget)
            {
                followingTarget = false;
                ResumeWaypointsFromClosest(); // Reprend les waypoints à partir du plus proche
            }
            else
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
                targetPosition = waypoints[currentWaypoint];
            }
        }
    }

    public void SetTarget(Vector2 newTarget)
    {
        targetPosition = newTarget;
        followingTarget = true; // Active le suivi ponctuel
    }

    private void ResumeWaypointsFromClosest()
    {
        // Trouve le waypoint le plus proche
        float closestDistance = float.MaxValue;
        int closestWaypointIndex = 0;

        for (int i = 0; i < waypoints.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, waypoints[i]);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestWaypointIndex = i;
            }
        }

        // Définit le waypoint le plus proche comme nouvelle cible
        currentWaypoint = closestWaypointIndex;
        targetPosition = waypoints[currentWaypoint];
    }

    public void GoToBeer(float X, float Y)
    {
        SetTarget(new Vector2(X, Y));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Obstacle collision detected!");
            // Arrêter ou ajuster la position en cas de collision
            Vector2 directionAwayFromObstacle = (Vector2)transform.position - collision.contacts[0].point;
            targetPosition = (Vector2)transform.position + directionAwayFromObstacle.normalized * 0.5f; // S'éloigne légèrement
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}

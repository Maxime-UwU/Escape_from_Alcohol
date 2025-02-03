using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement2 : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float firstSideLength;
    [SerializeField] public float secondSideLength;
    [SerializeField] public float thirdSideLength;
    [SerializeField] public float fourthSideLength;
    [SerializeField] public float UpDownLength;
    [SerializeField] public float secondUpLength;
    [SerializeField] public float thirdUpLength;

    private bool goingForward = true;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private Vector2[] waypoints;
    private int currentWaypoint = 0;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        startPosition = transform.position;

        waypoints = new Vector2[9];
        waypoints[0] = startPosition;
        waypoints[1] = startPosition + Vector2.right * firstSideLength;
        waypoints[2] = waypoints[1] + Vector2.up * UpDownLength;
        waypoints[3] = waypoints[2] - Vector2.up * UpDownLength;
        waypoints[4] = waypoints[3] - Vector2.right * secondSideLength;
        waypoints[5] = waypoints[4] + Vector2.up * secondUpLength;
        waypoints[6] = waypoints[5] + Vector2.right * thirdSideLength;
        waypoints[7] = waypoints[6] + Vector2.up * thirdUpLength;
        waypoints[8] = waypoints[7] - Vector2.right * fourthSideLength;

        targetPosition = waypoints[1];

        Vector2 direction = targetPosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void ChangeSpeed(float setSpeed)
    {
        speed = setSpeed;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        Vector2 direction = targetPosition - (Vector2)transform.position;
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if ((Vector2)transform.position == targetPosition)
        {
            if (goingForward)
            {
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Length)
                {
                    currentWaypoint = waypoints.Length - 2;
                    goingForward = false;
                }
            }
            else
            {
                currentWaypoint--;
                if (currentWaypoint < 0)
                {
                    currentWaypoint = 1;
                    goingForward = true;
                }
            }

            targetPosition = waypoints[currentWaypoint];
        }
    }

    public void GoToBeer(float X, float Y)
    {
        targetPosition = new Vector2(X, Y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("Ennemy"))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}

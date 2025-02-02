using UnityEngine;
using UnityEngine.SceneManagement;

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
    private Vector2[] waypoints;
    private int currentWaypoint = 0;
    public SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        startPosition = transform.position;

        waypoints = new Vector2[4];
        waypoints[0] = startPosition;
        waypoints[1] = startPosition - Vector2.up * secondSideLength;
        waypoints[2] = waypoints[1] + Vector2.right * sideLength;
        waypoints[3] = waypoints[2] + Vector2.up * secondSideLength;

        targetPosition = waypoints[1];
    }

    public void ChangeSpeed(float setSpeed)
    {
        speed = setSpeed;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if ((Vector2)transform.position == targetPosition)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            targetPosition = waypoints[currentWaypoint];

            if (currentWaypoint == 1 && firstRota)
            {
                this.transform.rotation *= Quaternion.Euler(0, 0, 90);
                this.transform.rotation *= Quaternion.Euler(0, 0, -90);
                firstRota = false;
            }
            else
            {
                this.transform.rotation *= Quaternion.Euler(0, 0, 90);

            }
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
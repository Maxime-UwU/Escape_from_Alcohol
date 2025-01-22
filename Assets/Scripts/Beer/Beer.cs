using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : MonoBehaviour
{
    private Movement m_movement;
    private AStarPathfinding m_pathFinding;

    public Transform startTransform;
    public Transform targetTransform;
    public Transform enemy;


    public float speed;
    public GameObject sound;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()

    {
        m_pathFinding = FindObjectOfType<AStarPathfinding>();
        m_movement = FindObjectOfType<Movement>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            startTransform = GameObject.FindGameObjectWithTag("Ennemy").transform;
            targetTransform = this.transform;
            enemy = GameObject.FindGameObjectWithTag("Ennemy").transform;

            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            this.transform.GetChild(0).gameObject.SetActive(true);
            sound = GameObject.FindGameObjectWithTag("Sound");
            sound.transform.localScale += new Vector3(1.5f, 0.9f, 1);

            if (enemy != null && startTransform != null && targetTransform != null)
            {
                // Positionnement des transformations
                startTransform.position = enemy.position;
                targetTransform.position = transform.position;

                // Configure le pathfinding
                m_pathFinding.SetTransform(startTransform, targetTransform);

                // Envoie l'ennemi directement à la position du projectile
                enemy.GetComponent<Movement>().SetTarget(this.transform.position);

                Destroy(gameObject);
            }
        }
    }
}

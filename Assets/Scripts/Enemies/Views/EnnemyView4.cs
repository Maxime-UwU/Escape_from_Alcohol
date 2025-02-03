using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnnemyView4 : MonoBehaviour
{

    [SerializeField]
    private Movement m_movement;

    public GameObject View;
    public GameObject Ennemy;

    // Start is called before the first frame update
    void Start()
    {
        View = GameObject.Find("View");
        Ennemy = GameObject.Find("Ennemy");
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_movement.ChangeSpeed(9f);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_movement.ChangeSpeed(8f);
        }
    }
}

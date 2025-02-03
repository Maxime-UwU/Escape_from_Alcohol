using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnnemyView3 : MonoBehaviour
{

    [SerializeField]
    private Movement3 m_movement3;

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
            m_movement3.ChangeSpeed(9f);
        }
        else
        {

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_movement3.ChangeSpeed(8f);
        }
    }
}

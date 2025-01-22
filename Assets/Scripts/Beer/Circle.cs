using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ennemy"))
        {
            //rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //this.transform.GetChild(0).gameObject.SetActive(true);
            //sound = GameObject.FindGameObjectWithTag("Sound");
            //sound.transform.localScale += new Vector3(1.5f, 0.9f, 1);
        }
    }
}

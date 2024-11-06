using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D() {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}

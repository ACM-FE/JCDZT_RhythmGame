using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,10);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity=new Vector2(0,-2);
    }
}

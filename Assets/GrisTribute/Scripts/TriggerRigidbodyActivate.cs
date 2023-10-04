using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRigidbodyActivate : MonoBehaviour
{
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            rb.isKinematic = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            rb.isKinematic = true;
            rb.gameObject.transform.parent = collision.transform;
        }
    }
}

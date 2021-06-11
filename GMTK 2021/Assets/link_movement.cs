using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class link_movement : MonoBehaviour
{
    public GameObject parent;
    public float distance;
    Rigidbody2D rb;
    public float speed;
    public float drag = 0.75f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.MovePosition(parent.transform.position - new Vector3(0, distance, 0));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 diff = parent.transform.position - gameObject.transform.position;
        if (diff.magnitude > distance) {
            diff.Normalize();
            rb.velocity = diff * speed;
        } else {
            rb.velocity = rb.velocity * drag;
        }
    }
}

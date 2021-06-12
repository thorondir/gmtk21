using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class link_movement : MonoBehaviour
{
    public GameObject parent;
    public float distance;
    Rigidbody2D rb;
    Animator anim;
    public float speed = 2.5f;
    public float maxSpeed = 5f;
    public float drag = 0.75f;

    Vector2 direction;
    Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;
        anim.SetFloat("x_velocity", velocity.x);
        anim.SetFloat("y_velocity", velocity.y);
    }

    // move towards some position if at some distance from a reference point
    public void moveTowards(Vector2 position, Vector2 reference, float distance) {
        if ((reference - (Vector2) transform.position).magnitude > distance) {
            direction = position - (Vector2) transform.position;
            direction.Normalize();
            if (rb.velocity.magnitude < maxSpeed)
                rb.velocity += direction * speed;
            else 
                rb.velocity = direction * maxSpeed;
        } else {
            rb.velocity = rb.velocity * drag;
        }
    }
}

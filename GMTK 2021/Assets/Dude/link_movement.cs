using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class link_movement : MonoBehaviour
{
    public GameObject parent;
    public float distance;
    public Health hp;
    Rigidbody2D rb;
    Animator anim;
    public float speed = 2.5f;
    public float maxSpeed = 5f;
    public float drag = 0.75f;
    public double animationThreshold = 0.2;

    Vector2 direction;
    Vector2 velocity;

    public GameObject SndManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hp = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;
        if (velocity.x > animationThreshold) {
            anim.SetInteger("x_movement", 1);
        } else if (velocity.x < -animationThreshold) {
            anim.SetInteger("x_movement", -1);
        } else {
            anim.SetInteger("x_movement", 0);
        }
        if (velocity.y > animationThreshold) {
            anim.SetInteger("y_movement", 1);
        } else if (velocity.y < -animationThreshold) {
            anim.SetInteger("y_movement", -1);
        } else {
            anim.SetInteger("y_movement", 0);
        }
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

    void GotHit() {
        
        SndManager.GetComponent<SoundManager>().playManPain();
        if (hp.health <= 0)  {
            anim.SetBool("is_dead", true);
            speed *= 0.75f;
            maxSpeed *= 0.75f;
            Destroy(GetComponent<Health>());
        }
    }
}

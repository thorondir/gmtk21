using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class head_movement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float drag = 0.75f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xinput = Input.GetAxis("Horizontal");
        float yinput = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(xinput * speed, yinput * speed);
        if (xinput == 0 && yinput == 0) {
            rb.velocity = rb.velocity * drag;
        }
    }
}

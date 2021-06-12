using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainStretcher : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float length, angle;
    public float angleOffset;
    Vector3 delta;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Join(GameObject a, GameObject b) {
        delta = a.transform.position - b.transform.position;
        length = delta.magnitude;
        angle = Mathf.Rad2Deg * Mathf.Atan2(delta.y, delta.x);
        spriteRenderer.size = new Vector2(length, 0.15f);
        transform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
        transform.position = ((a.transform.position + b.transform.position) / 2) + new Vector3(0,0,0.8f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOrderAuto : MonoBehaviour
{
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.z = transform.position.y;

        transform.position = pos;
        
        
    }
}

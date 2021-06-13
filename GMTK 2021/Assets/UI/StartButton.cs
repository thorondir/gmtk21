using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{

    public Camera cam;

    bool panning = false;

    public double MAX_PAN;
    public int PAN_SPEED = 1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (panning)
        {
            cam.transform.position += new Vector3(PAN_SPEED, 0, 0);
            if (cam.transform.position.y >= MAX_PAN)
            {
                cam.transform.position = new Vector3(0, (float)MAX_PAN, cam.transform.position.z);
                panning = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == GetComponent<chainmanager>().chain[0])
        {
            panUp();
        }
    }

    void panUp()
    {
        panning = true;
    }
}

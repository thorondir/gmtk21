using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{

    bool panning = false;

    public float MAX_PAN = 10;
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
            Camera.main.transform.position += new Vector3(PAN_SPEED*Time.deltaTime, 0, 0);
            if (Camera.main.transform.position.y >= MAX_PAN)
            {
                Camera.main.transform.position = new Vector3(0, (float)MAX_PAN, Camera.main.transform.position.z);
                panning = false;

                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null)
            return;
        if (other == FindObjectOfType<chainmanager>().chain[0])
        {
            panUp();
        }
    }

    void panUp()
    {
        panning = true;
    }
}

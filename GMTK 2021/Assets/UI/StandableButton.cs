using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandableButton : MonoBehaviour
{

    public GameObject displayObject;


    // Start is called before the first frame update
    void Start()
    {
        displayObject = Instantiate(displayObject);
        displayObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        displayObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == GetComponent<chainmanager>().chain[0])
        {
            displayThing();
        }
    }

    void displayThing()
    {
        displayObject.SetActive(true);

    }
}

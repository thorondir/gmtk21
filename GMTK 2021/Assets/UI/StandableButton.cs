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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null)
            return;
                Debug.Log(other);
        Debug.Log(FindObjectOfType<chainmanager>());

        if (other.gameObject == FindObjectOfType<chainmanager>().chain[0])
        {
            displayThing();
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other == null)
            return;
        if (other.gameObject == FindObjectOfType<chainmanager>().chain[0])
        {
            hideThing();
        }
    }
    void displayThing()
    {
        displayObject.SetActive(true);

    }
    void hideThing()
    {
        displayObject.SetActive(false);

    }
}


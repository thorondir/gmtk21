using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockfall : MonoBehaviour
{
    public bool isFalling = false;

    // Update is called once per frame
    void Update()
    {
        if (isFalling)
        {
            transform.position -= new Vector3(0, -10 * Time.deltaTime, 0);
        }
    }
}

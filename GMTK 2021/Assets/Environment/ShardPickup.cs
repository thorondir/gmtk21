using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardPickup : MonoBehaviour
{
    
    public GameObject indicator;

    void Awake()
    {
        //Debug.Log(transform.position.x.ToString() +", "+ transform.position.y.ToString());
        // this typecast is absolute gold
        Vector3 newPos = new Vector3(0, (float).8, 0);
        GameObject myInd = Instantiate(indicator, newPos, Quaternion.identity, transform);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<link_movement>() != null)
        {
            if (other.GetComponent<link_movement>().CollectWeapon())
                Destroy(gameObject);
        }
    }
}

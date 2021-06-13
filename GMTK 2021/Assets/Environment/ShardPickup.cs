using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardPickup : MonoBehaviour
{
    
    public GameObject indicator;

    void Awake()
    {
        //Debug.Log(transform.position.x.ToString() +", "+ transform.position.y.ToString());
        Vector3 newPos = transform.position + new Vector3(0, (float).8, -8);
        GameObject myInd = Instantiate(indicator, newPos, Quaternion.identity, transform);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Stabbing>() != null)
        {
            if (other.GetComponent<Stabbing>().CollectWeapon())
                Destroy(gameObject);
        }
    }
}

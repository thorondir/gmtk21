using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardPickup : MonoBehaviour
{
    
    public GameObject indicator;

    void Awake()
    {
        GameObject myInd = Instantiate(indicator, GetComponent<Transform>());
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
